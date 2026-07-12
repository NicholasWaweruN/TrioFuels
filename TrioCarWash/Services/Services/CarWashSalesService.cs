using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.CarWash;
using DataAccessLayer.EntityModels.Grleamify;
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
		CarWashPaymetMethod.Cash
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

		if (request.PaymentMethod == CarWashPaymetMethod.Cash)
		{
			if (request.AmountReceived is 0 || request.AmountReceived < total)
				return ServiceResponse<SaleResponseDto>.Error("Amount received must cover the total");
		}

		if (request.PaymentMethod == CarWashPaymetMethod.Mpesa
			&& string.IsNullOrWhiteSpace(request.PhoneNumber))
			return ServiceResponse<SaleResponseDto>.Error("Phone number is required for M-Pesa payment");

		decimal change = request.PaymentMethod == CarWashPaymetMethod.Cash ? request.AmountReceived! - total : 0;

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
				TotalAmount = total,
				PaymentMethod = request.PaymentMethod,
				AmountReceived = request.AmountReceived,
				Change = change,
				PhoneNumber = request.PhoneNumber,
				MpesaReference = null, // TODO: wire real Daraja STK push here, don't fabricate a reference
				ReceiptNumber = GenerateReceiptNumber(shift.Id)
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
			await _db.SaveChangesAsync();
			await tx.CommitAsync();

			return new SaleResponseDto
			{
				SaleId = sale.Id,
				ReceiptNumber = sale.ReceiptNumber,
				Total = total,
				Change = change,
				PaymentMethod = request.PaymentMethod,
				MpesaReference = sale.MpesaReference,
				CreatedAt = sale.DateCreated,
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
			Change = t.Change,
			PaymentMethod = t.PaymentMethod,
			MpesaReference = t.MpesaReference,
			CreatedAt = t.DateCreated,
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