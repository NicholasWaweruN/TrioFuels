using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.DTOs.CarWash;
using DataAccessLayer.EntityModels.Grleamify;
using Microsoft.EntityFrameworkCore;

namespace TrioCarWash.Services.Services;

public interface ICarWashSalesService
{
	Task<ServiceResponse<List<ProductDto>>> GetProductsAsync();
	Task<ServiceResponse<SaleResponseDto>> CreateSaleAsync(string userCode, CreateSaleRequestDto request);
	Task<ServiceResponse<List<SaleResponseDto>>> GetSalesHistoryAsync(string userCode, DateTime? from, DateTime? to);
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

	public async Task<ServiceResponse<List<ProductDto>>> GetProductsAsync()
	{
		var products = await _db.CarWashProducts
			.AsNoTracking()
			.Where(p => p.IsActive)
			.OrderBy(p => p.Name)
			.Select(p => new ProductDto { ProductId = p.Id, Name = p.Name, Price = p.Price })
			.ToListAsync();

		return ServiceResponse<List<ProductDto>>.Success("OK", products);
	}

	public async Task<ServiceResponse<SaleResponseDto>> CreateSaleAsync(
		string userCode, CreateSaleRequestDto request)
	{
		if (request.Items.Count == 0)
			return ServiceResponse<SaleResponseDto>.Error("Sale must have at least one item");

		if (!AllowedPaymentMethods.Contains(request.PaymentMethod))
			return ServiceResponse<SaleResponseDto>.Error("Unsupported payment method");

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

		var total = request.Items.Sum(i => products[i.ProductId].Price * i.Quantity);

		if (request.PaymentMethod == CarWashPaymetMethod.Cash)
		{
			if (request.AmountReceived is 0 || request.AmountReceived < total)
				return ServiceResponse<SaleResponseDto>.Error("Amount received must cover the total");
		}

		if (request.PaymentMethod == CarWashPaymetMethod.Mpesa
			&& string.IsNullOrWhiteSpace(request.PhoneNumber))
			return ServiceResponse<SaleResponseDto>.Error("Phone number is required for M-Pesa payment");

		decimal change = request.PaymentMethod == CarWashPaymetMethod.Cash
			? request.AmountReceived! - total
			: 0;

		using var tx = await _db.Database.BeginTransactionAsync();

		var sale = new CarWashTransaction
		{
			UserCode = userCode,
			ShiftId = shift.Id,
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
		catch (DbUpdateException) when (IsUniqueViolation())
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
				UnitPrice = products[item.ProductId].Price // snapshot, not live price
			});
		}
		await _db.SaveChangesAsync();
		await tx.CommitAsync();

		var dto = new SaleResponseDto
		{
			SaleId = sale.Id,
			ReceiptNumber = sale.ReceiptNumber,
			Total = total,
			Change = change,
			PaymentMethod = request.PaymentMethod,
			MpesaReference = sale.MpesaReference,
			CreatedAt = sale.DateCreated,
			Items = request.Items.Select(i => new SaleItemLineDto
			{
				ProductName = products[i.ProductId].Name,
				Quantity = i.Quantity,
				UnitPrice = products[i.ProductId].Price
			}).ToList()
		};

		return ServiceResponse<SaleResponseDto>.Success("Sale completed", dto);
	}

	public async Task<ServiceResponse<List<SaleResponseDto>>> GetSalesHistoryAsync(
		string userCode, DateTime? from, DateTime? to)
	{
		var query = _db.CarWashTransactions
			.AsNoTracking()
			.Include(t => t.Items).ThenInclude(i => i.Product)
			.Where(t => t.UserCode == userCode && !t.IsReversed);

		if (from.HasValue) query = query.Where(t => t.DateCreated >= from.Value);
		if (to.HasValue) query = query.Where(t => t.DateCreated <= to.Value);

		var sales = await query
			.OrderByDescending(t => t.DateCreated)
			.Take(200) // TODO: real pagination
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

	private static string GenerateReceiptNumber(long shiftId) =>
		$"CW{shiftId:D4}{DateTime.UtcNow:HHmmssfff}";

	private static bool IsUniqueViolation() => true; // TODO: inspect inner exception for the real DB unique-constraint error code
}