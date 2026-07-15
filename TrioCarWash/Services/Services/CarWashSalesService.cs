using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.CarWash;
using DataAccessLayer.EntityModels.Grleamify;
using DataAccessLayer.EntityModels.Transactions;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace TrioCarWash.Services.Services;

public interface ICarWashSalesService
{
	Task<ServiceResponse<List<VehicleTypeDto>>> GetVehicleTypesAsync();
	Task<ServiceResponse<List<ProductDto>>> GetProductsAsync(long vehicleTypeId);
	Task<ServiceResponse<SaleResponseDto>> CreateSaleAsync(string userCode, CreateSaleRequestDto request);
	Task<ServiceResponse<List<SaleResponseDto>>> GetSalesHistoryAsync(long shiftId);
}

public class CarWashSalesService : ICarWashSalesService
{
	private static readonly HashSet<int> AllowedPaymentMethods = new()
	{
		CarWashPaymetMethod.Mpesa,
		CarWashPaymetMethod.Cash,
		CarWashPaymetMethod.Credit
		// CarWashPaymetMethod.Voucher intentionally excluded — not wired yet
	};

	private readonly OTOContext _db;
	private readonly ICarWashShiftService _shiftService;

	public CarWashSalesService(OTOContext db, ICarWashShiftService shiftService)
	{
		_db = db;
		_shiftService = shiftService;
	}

	public async Task<ServiceResponse<List<VehicleTypeDto>>> GetVehicleTypesAsync()
	{
		var types = await _db.VehicleTypes
			.AsNoTracking()
			.Where(v => v.IsActive)
			.OrderBy(v => v.Name)
			.Select(v => new VehicleTypeDto { VehicleTypeId = v.Id, Name = v.Name })
			.ToListAsync();

		return ServiceResponse<List<VehicleTypeDto>>.Success("OK", types);
	}

	public async Task<ServiceResponse<List<ProductDto>>> GetProductsAsync(long vehicleTypeId)
	{
		var vehicleTypeExists = await _db.VehicleTypes.AnyAsync(v => v.Id == vehicleTypeId && v.IsActive);
		if (!vehicleTypeExists)
			return ServiceResponse<List<ProductDto>>.Error("Vehicle type not found or inactive");

		// base products, left-joined against this vehicle type's price overrides
		var products = await _db.CarWashProducts
			.AsNoTracking()
			.Where(p => p.IsActive)
			.OrderBy(p => p.Name)
			.Select(p => new ProductDto
			{
				ProductId = p.Id,
				Name = p.Name,
				Price = p.VehicleTypePrices
					.Where(vp => vp.VehicleTypeId == vehicleTypeId)
					.Select(vp => (decimal?)vp.Price)
					.FirstOrDefault() ?? p.Price
			})
			.ToListAsync();

		return ServiceResponse<List<ProductDto>>.Success("OK", products);
	}

	public async Task<ServiceResponse<SaleResponseDto>> CreateSaleAsync(string userCode, CreateSaleRequestDto request)
	{
		if (string.IsNullOrEmpty(request.VehiceRegistrationNumber))
			return ServiceResponse<SaleResponseDto>.Error("Vehicle registration number must be present");

		if (request.Items.Count == 0)
			return ServiceResponse<SaleResponseDto>.Error("Sale must have at least one item");

		if (!AllowedPaymentMethods.Contains(request.PaymentMethod))
			return ServiceResponse<SaleResponseDto>.Error("Unsupported payment method");

		var vehicleType = await _db.VehicleTypes
			.FirstOrDefaultAsync(v => v.Id == request.VehicleTypeId && v.IsActive);
		if (vehicleType == null)
			return ServiceResponse<SaleResponseDto>.Error("Vehicle type not found or inactive");

		var shift = await _shiftService.GetActiveShiftAsync(userCode);
		if (shift == null)
			return ServiceResponse<SaleResponseDto>.Error("No active shift — open a shift before selling");

		var productIds = request.Items.Select(i => i.ProductId).Distinct().ToList();
		var products = await _db.CarWashProducts
			.Where(p => productIds.Contains(p.Id) && p.IsActive)
			.ToDictionaryAsync(p => p.Id);

		if (products.Count != productIds.Count)
			return ServiceResponse<SaleResponseDto>.Error("One or more products were not found or inactive");

		if (request.Items.Any(i => i.Quantity <= 0))
			return ServiceResponse<SaleResponseDto>.Error("Quantity must be greater than zero");

		// per-vehicle-type overrides for just the products in this sale
		var priceOverrides = await _db.CarWashProductPrices
			.Where(vp => vp.VehicleTypeId == request.VehicleTypeId && productIds.Contains(vp.ProductId))
			.ToDictionaryAsync(vp => vp.ProductId, vp => vp.Price);

		decimal PriceFor(long productId) =>
			priceOverrides.TryGetValue(productId, out var overridePrice) ? overridePrice : products[productId].Price;

		var total = request.Items.Sum(i => PriceFor(i.ProductId) * i.Quantity);

		// ---------------------------------------------------------------------
		// Customer lookup — drives standing discount + credit eligibility.
		// A miss is never a blocking error (mirrors the Android "walk-in" UX);
		// it only becomes an error later if the attendant chose Credit and
		// there's no credit-eligible customer to charge it to.
		// ---------------------------------------------------------------------
		CarwashCustomer? customer = null;
		if (!string.IsNullOrWhiteSpace(request.CustomerPhoneNumber))
		{
			customer = await _db.CarwashCustomers
				.FirstOrDefaultAsync(c => c.PhoneNumber == request.CustomerPhoneNumber);
		}

		// ---------------------------------------------------------------------
		// Discounts. Two independent sources, both applied against the gross
		// catalog total, combined and floored at zero:
		//   - standingDiscount: automatic, tied to the customer record, applies
		//     regardless of payment method.
		//   - negotiatedDiscount: a one-off knocked off at the till. Android only
		//     ever collects this inside the Cash dialog, so it's enforced
		//     server-side too rather than trusting the client.
		// ---------------------------------------------------------------------
		var standingDiscount = customer?.IsDiscountCustomer == true
			? customer.DiscountAmount
			: 0m;

		var negotiatedDiscount = request.NegotiatedDiscount ?? 0m;
		if (negotiatedDiscount > 0m && request.PaymentMethod != CarWashPaymetMethod.Cash)
			return ServiceResponse<SaleResponseDto>.Error("Negotiated discount can only be applied to cash payments");

		if (negotiatedDiscount < 0m)
			return ServiceResponse<SaleResponseDto>.Error("Discount cannot be negative");

		var discountTotal = standingDiscount + negotiatedDiscount;
		var amountDue = Math.Max(0m, total - discountTotal);

		// ---------------------------------------------------------------------
		// Payment-method specific validation. Nothing is written to the DB yet —
		// we only mutate (mark M-Pesa used / bump credit balance) once we're
		// inside the transaction below, right before the sale itself is saved.
		// ---------------------------------------------------------------------
		MpesaTransaction? mpesaTransaction = null;

		if (request.PaymentMethod == CarWashPaymetMethod.Cash)
		{
			var disc = request.NegotiatedDiscount;

			if (request.AmountReceived == 0m || request.AmountReceived < amountDue-disc)
				return ServiceResponse<SaleResponseDto>.Error("Amount received must cover the total");
		}
		else if (request.PaymentMethod == CarWashPaymetMethod.Mpesa)
		{
			if (string.IsNullOrWhiteSpace(request.MpesaCode) || request.MpesaCode.Length != 10)
				return ServiceResponse<SaleResponseDto>.Error("A valid 10-character M-Pesa code is required");

			var mpesaCode = request.MpesaCode.Trim().ToUpperInvariant();

			// Attendants enter whatever code is on the confirmation SMS. For C2B
			// payments that's TransID; for STK Push it's MpesaReceiptNumber. Check both.
			mpesaTransaction = await _db.MpesaTransactions
				.FirstOrDefaultAsync(m => m.TransID == mpesaCode || m.MpesaReceiptNumber == mpesaCode);

			if (mpesaTransaction == null)
				return ServiceResponse<SaleResponseDto>.Error("M-Pesa code not recognized — check the confirmation SMS and try again");

			// Status: 0=Pending, 1=Success, 2=Failed. Only a confirmed Success
			// payment can be applied to a sale.
			if (mpesaTransaction.Status == 0)
				return ServiceResponse<SaleResponseDto>.Error("This M-Pesa payment hasn't been confirmed yet — please wait a moment and try again");

			if (mpesaTransaction.Status == 2)
				return ServiceResponse<SaleResponseDto>.Error("This M-Pesa payment failed and cannot be used");

			// Status has no "used" state of its own, so consumption is tracked via
			// ShiftNumber: empty/null means unused, populated means it's already
			// been applied to a sale on that shift.
			if (!string.IsNullOrWhiteSpace(mpesaTransaction.ShiftNumber))
				return ServiceResponse<SaleResponseDto>.Error("This M-Pesa code has already been used on another sale");

			if (mpesaTransaction.TransAmount < amountDue)
				return ServiceResponse<SaleResponseDto>.Error(
					$"M-Pesa payment of {mpesaTransaction.TransAmount:N0} does not cover the amount due of {amountDue:N0}");
		}
		else if (request.PaymentMethod == CarWashPaymetMethod.Credit)
		{
			if (customer == null || !customer.IsCreditCustomer)
				return ServiceResponse<SaleResponseDto>.Error("No credit-eligible customer found for this phone number");

			if (customer.CurrentBalance + amountDue > customer.CreditLimit)
				return ServiceResponse<SaleResponseDto>.Error(
					$"This sale would exceed the customer's credit limit ({customer.CurrentBalance:N0} + {amountDue:N0} > {customer.CreditLimit:N0})");
		}

		decimal change = request.PaymentMethod == CarWashPaymetMethod.Cash
			? request.AmountReceived - amountDue
			: 0;

		// EnableRetryOnFailure requires the whole transaction to run inside
		// the execution strategy so a transient failure can retry the entire
		// unit atomically — a bare BeginTransactionAsync() throws.
		var strategy = _db.Database.CreateExecutionStrategy();

		var dto = await strategy.ExecuteAsync(async () =>
		{
			using var tx = await _db.Database.BeginTransactionAsync();

			var sale = new CarWashTransaction
			{
				UserCode = userCode,
				ShiftId = shift.Id,
				VehicleTypeId = request.VehicleTypeId,
				CustomerId = customer?.Id,
				TotalAmount = total,
				DiscountAmount = discountTotal,
				AmountDue = amountDue,
				PaymentMethod = request.PaymentMethod,
				AmountReceived = request.PaymentMethod == CarWashPaymetMethod.Cash ? request.AmountReceived : 0m,
				Change = change,
				PhoneNumber = request.CustomerPhoneNumber,
				MpesaReference = string.IsNullOrWhiteSpace(mpesaTransaction?.MpesaReceiptNumber)
					? mpesaTransaction?.TransID
					: mpesaTransaction.MpesaReceiptNumber,
				ReceiptNumber = GenerateReceiptNumber(shift.Id),
				VehicleRegistrationNumber = request.VehiceRegistrationNumber,
			};

			_db.CarWashTransactions.Add(sale);

			try
			{
				await _db.SaveChangesAsync();
			}
			catch (DbUpdateException ex) when (IsUniqueViolation(ex))
			{
				// extremely unlikely collision on receipt number — regenerate once and retry
				sale.ReceiptNumber = GenerateReceiptNumber(shift.Id);
				await _db.SaveChangesAsync();
			}

			foreach (var item in request.Items)
			{
				_db.CarWashTransactionItems.Add(new CarWashTransactionItem
				{
					UserCode = userCode,
					TransactionId = sale.Id,
					ProductId = item.ProductId,
					Quantity = item.Quantity,
					UnitPrice = PriceFor(item.ProductId) // snapshot for this vehicle type, not live price
				});
			}

			// Mark the M-Pesa code as consumed so it can't be reused on a second
			// sale. Done inside the same transaction as the sale insert so a
			// failure anywhere rolls back the "used" flag too. Status is left
			// untouched (still 1/Success) — ShiftNumber is the consumption marker.
			if (mpesaTransaction != null)
			{
				mpesaTransaction.ShiftNumber = shift.Id.ToString();
			}

			// Post the charge to the customer's credit account.
			if (request.PaymentMethod == CarWashPaymetMethod.Credit && customer != null)
			{
				customer.CurrentBalance += amountDue;
			}

			await _db.SaveChangesAsync();
			await tx.CommitAsync();

			return new SaleResponseDto
			{
				SaleId = sale.Id,
				ReceiptNumber = sale.ReceiptNumber,
				Total = total,
				DiscountAmount = discountTotal,
				AmountDue = amountDue,
				Change = change,
				PaymentMethod = request.PaymentMethod,
				MpesaReference = sale.MpesaReference,
				CreatedAt = sale.DateCreated,
				VehicleRegistrationNumber = sale.VehicleRegistrationNumber,
				Items = [.. request.Items.Select(i => new SaleItemLineDto
				{
					ProductName = products[i.ProductId].Name,
					Quantity = i.Quantity,
					UnitPrice = PriceFor(i.ProductId)
				})]
			};
		});

		return ServiceResponse<SaleResponseDto>.Success("Sale completed", dto);
	}

	public async Task<ServiceResponse<List<SaleResponseDto>>> GetSalesHistoryAsync(long shiftId)
	{
		if (shiftId <= 0)
			return ServiceResponse<List<SaleResponseDto>>.Error("A valid shiftId is required");

		var shiftBelongsToUser = await _db.CarWashShifts.AnyAsync(s => s.Id == shiftId);

		if (!shiftBelongsToUser)
			return ServiceResponse<List<SaleResponseDto>>.Error("Shift not found");

		var query = _db.CarWashTransactions
			.AsNoTracking()
			.Include(t => t.Items).ThenInclude(i => i.Product)
			.Where(t => t.ShiftId == shiftId && !t.IsReversed);

		var sales = await query
			.OrderByDescending(t => t.DateCreated)
			.Take(200)
			.ToListAsync();

		var result = sales.Select(t => new SaleResponseDto
		{
			SaleId = t.Id,
			ReceiptNumber = t.ReceiptNumber,
			Total = t.TotalAmount,
			DiscountAmount = t.DiscountAmount,
			AmountDue = t.AmountDue,
			Change = t.Change,
			PaymentMethod = t.PaymentMethod,
			MpesaReference = t.MpesaReference,
			CreatedAt = t.DateCreated,
			VehicleRegistrationNumber = t.VehicleRegistrationNumber,
			Items = t.Items.Select(i => new SaleItemLineDto
			{
				ProductName = i.Product.Name,
				Quantity = i.Quantity,
				UnitPrice = i.UnitPrice
			}).ToList()
		}).ToList();

		return ServiceResponse<List<SaleResponseDto>>.Success("OK", result);
	}

	private static string GenerateReceiptNumber(long shiftId) => $"CW{shiftId:D4}{DateTime.UtcNow:HHmmssfff}";

	private static bool IsUniqueViolation(Exception ex) =>
		ex.InnerException is PostgresException pg && pg.SqlState == PostgresErrorCodes.UniqueViolation;
}