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
	private readonly ICarWashPackageService _packageService;

	public CarWashSalesService(OTOContext db, ICarWashShiftService shiftService, ICarWashPackageService packageService)
	{
		_db = db;
		_shiftService = shiftService;
		_packageService = packageService;
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

		if (request.Items.Any(i => i.Quantity <= 0))
			return ServiceResponse<SaleResponseDto>.Error("Quantity must be greater than zero");

		if (request.Items.Any(i => i.ProductId.HasValue == i.PackageId.HasValue))
			return ServiceResponse<SaleResponseDto>.Error("Each sale item must specify exactly one of ProductId or PackageId");

		var vehicleType = await _db.VehicleTypes
			.FirstOrDefaultAsync(v => v.Id == request.VehicleTypeId && v.IsActive);
		if (vehicleType == null)
			return ServiceResponse<SaleResponseDto>.Error("Vehicle type not found or inactive");

		var shift = await _shiftService.GetActiveShiftAsync(userCode);
		if (shift == null)
			return ServiceResponse<SaleResponseDto>.Error("No active shift — open a shift before selling");

		var directItems = request.Items.Where(i => i.ProductId.HasValue).ToList();
		var packageItems = request.Items.Where(i => i.PackageId.HasValue).ToList();

		// ---------------------------------------------------------------------
		// Resolve packages up front so we know every product ID we need
		// pricing for — both standalone lines and everything nested inside
		// a package.
		// ---------------------------------------------------------------------
		var packageIds = packageItems.Select(i => i.PackageId!.Value).Distinct().ToList();
		var packages = packageIds.Count > 0
			? await _packageService.GetActivePackagesForSaleAsync(packageIds)
			: new Dictionary<long, CarWashPackage>();

		if (packages.Count != packageIds.Count)
			return ServiceResponse<SaleResponseDto>.Error("One or more packages were not found or inactive");

		foreach (var pkg in packages.Values)
		{
			if (!pkg.VehicleTypePrices.Any(vp => vp.VehicleTypeId == request.VehicleTypeId))
				return ServiceResponse<SaleResponseDto>.Error(
					$"Package '{pkg.Name}' has no price configured for this vehicle type");
		}

		var directProductIds = directItems.Select(i => i.ProductId!.Value);
		var packageProductIds = packages.Values.SelectMany(p => p.Items.Select(i => i.ProductId));
		var productIds = directProductIds.Concat(packageProductIds).Distinct().ToList();

		var products = productIds.Count > 0
			? await _db.CarWashProducts
				.Where(p => productIds.Contains(p.Id) && p.IsActive)
				.ToDictionaryAsync(p => p.Id)
			: new Dictionary<long, CarWashProduct>();

		if (products.Count != productIds.Count)
			return ServiceResponse<SaleResponseDto>.Error("One or more products were not found or inactive");

		var priceOverrides = await _db.CarWashProductPrices
			.Where(vp => vp.VehicleTypeId == request.VehicleTypeId && productIds.Contains(vp.ProductId))
			.ToDictionaryAsync(vp => vp.ProductId, vp => vp.Price);

		decimal PriceFor(long productId) =>
			priceOverrides.TryGetValue(productId, out var overridePrice) ? overridePrice : products[productId].Price;

		// ---------------------------------------------------------------------
		// Expand each package line into per-product allocations that sum
		// exactly to (bundle price * quantity). Proportional split by each
		// product's normal-price share of the bundle's individual total;
		// the last product in the package absorbs the rounding remainder so
		// the allocated lines always add up exactly to the bundle price.
		// ---------------------------------------------------------------------
		var expandedPackageLines = new List<(SaleItemDto SourceItem, CarWashPackage Package, Guid InstanceId,
											  long ProductId, decimal UnitPrice)>();

		foreach (var item in packageItems)
		{
			var pkg = packages[item.PackageId!.Value];
			var bundlePrice = pkg.VehicleTypePrices.First(vp => vp.VehicleTypeId == request.VehicleTypeId).Price;

			var normalPrices = pkg.Items.Select(pi => (pi.ProductId, Normal: PriceFor(pi.ProductId))).ToList();
			var individualTotal = normalPrices.Sum(x => x.Normal);

			if (individualTotal <= 0)
				return ServiceResponse<SaleResponseDto>.Error($"Package '{pkg.Name}' has no valid product pricing to allocate against");

			var instanceId = Guid.NewGuid();
			decimal allocatedSoFar = 0m;

			for (int i = 0; i < normalPrices.Count; i++)
			{
				var (productId, normal) = normalPrices[i];
				decimal unitPrice;

				if (i == normalPrices.Count - 1)
					unitPrice = bundlePrice - allocatedSoFar; // absorbs rounding remainder
				else
				{
					unitPrice = Math.Round(normal / individualTotal * bundlePrice, 2, MidpointRounding.AwayFromZero);
					allocatedSoFar += unitPrice;
				}

				expandedPackageLines.Add((item, pkg, instanceId, productId, unitPrice));
			}
		}

		var directTotal = directItems.Sum(i => PriceFor(i.ProductId!.Value) * i.Quantity);
		var packageTotal = packageItems.Sum(item =>
			packages[item.PackageId!.Value].VehicleTypePrices
				.First(vp => vp.VehicleTypeId == request.VehicleTypeId).Price * item.Quantity);

		var total = directTotal + packageTotal;

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
			if (request.AmountReceived == 0m || request.AmountReceived < amountDue - disc)
				return ServiceResponse<SaleResponseDto>.Error("Amount received must cover the total");
		}
		else if (request.PaymentMethod == CarWashPaymetMethod.Mpesa)
		{
			request.MpesaCode = request.MpesaCode!.Trim();
			if (string.IsNullOrWhiteSpace(request.MpesaCode) || request.MpesaCode.Length != 10)
				return ServiceResponse<SaleResponseDto>.Error("A valid 10-character M-Pesa code is required");

			var mpesaCode = request.MpesaCode.Trim().ToUpperInvariant();

			mpesaTransaction = await _db.MpesaTransactions
				.FirstOrDefaultAsync(m => m.TransID == mpesaCode || m.MpesaReceiptNumber == mpesaCode);

			if (mpesaTransaction == null)
				return ServiceResponse<SaleResponseDto>.Error("M-Pesa code not recognized — check the confirmation SMS and try again");

			if (mpesaTransaction.Status == 0)
				return ServiceResponse<SaleResponseDto>.Error("This M-Pesa payment hasn't been confirmed yet — please wait a moment and try again");

			if (mpesaTransaction.Status == 2)
				return ServiceResponse<SaleResponseDto>.Error("This M-Pesa payment failed and cannot be used");

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
				sale.ReceiptNumber = GenerateReceiptNumber(shift.Id);
				await _db.SaveChangesAsync();
			}

			foreach (var item in directItems)
			{
				_db.CarWashTransactionItems.Add(new CarWashTransactionItem
				{
					UserCode = userCode,
					TransactionId = sale.Id,
					ProductId = item.ProductId!.Value,
					Quantity = item.Quantity,
					UnitPrice = PriceFor(item.ProductId.Value)
				});
			}

			foreach (var (SourceItem, Package, InstanceId, ProductId, UnitPrice) in expandedPackageLines)
			{
				_db.CarWashTransactionItems.Add(new CarWashTransactionItem
				{
					UserCode = userCode,
					TransactionId = sale.Id,
					ProductId = ProductId,
					Quantity = SourceItem.Quantity, // number of bundles sold
					UnitPrice = UnitPrice,           // this product's slice of the bundle price, per bundle
					PackageId = Package.Id,
					PackageInstanceId = InstanceId
				});
			}

			mpesaTransaction?.ShiftNumber = shift.Id.ToString();

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
				Items =
				[
					.. directItems.Select(i => new SaleItemLineDto
					{
						ProductName = products[i.ProductId!.Value].Name,
						Quantity = i.Quantity,
						UnitPrice = PriceFor(i.ProductId.Value)
					}),
					.. expandedPackageLines.Select(line => new SaleItemLineDto
					{
						ProductName = products[line.ProductId].Name,
						Quantity = line.SourceItem.Quantity,
						UnitPrice = line.UnitPrice,
						PackageId = line.Package.Id,
						PackageName = line.Package.Name,
						PackageInstanceId = line.InstanceId
					})
				]
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
			.Include(t => t.Items).ThenInclude(i => i.Package)
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
				UnitPrice = i.UnitPrice,
				PackageId = i.PackageId,
				PackageName = i.Package?.Name,
				PackageInstanceId = i.PackageInstanceId
			}).ToList()
		}).ToList();

		return ServiceResponse<List<SaleResponseDto>>.Success("OK", result);
	}

	private static string GenerateReceiptNumber(long shiftId) => $"CW{shiftId:D4}{DateTime.UtcNow:HHmmssfff}";

	private static bool IsUniqueViolation(Exception ex) =>
		ex.InnerException is PostgresException pg && pg.SqlState == PostgresErrorCodes.UniqueViolation;
}