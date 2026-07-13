namespace DataAccessLayer.DTOs.CarWash;

public class ProductDto
{
	public long ProductId { get; set; }
	public string Name { get; set; } = string.Empty;
	public decimal Price { get; set; }
}

public class OpenShiftRequestDto
{
	public string ShiftName { get; set; } = string.Empty; // "Morning Shift"
}

public class ShiftDto
{
	public long ShiftId { get; set; }
	public string ShiftName { get; set; } = string.Empty;
	public DateTime OpenedAt { get; set; }
}

public class SaleItemRequestDto
{
	public long ProductId { get; set; }
	public int Quantity { get; set; } = 1;
}

public class CreateSaleRequestDto
{
	public List<SaleItemRequestDto> Items { get; set; } = new();
	public int PaymentMethod { get; set; } // CarWashPaymetMethod.Cash or .Mpesa only, for now
	public decimal AmountReceived { get; set; } = 0m; // cash only
	public string? PhoneNumber { get; set; }      // M-Pesa STK only
	public long VehicleTypeId { get; set; }
	public string VehiceRegistrationNumber { get; set; } = string.Empty;
}

public class SaleItemLineDto
{
	public string ProductName { get; set; } = string.Empty;
	public int Quantity { get; set; }
	public decimal UnitPrice { get; set; }
	public decimal LineTotal => UnitPrice * Quantity;
}

public class SaleResponseDto
{
	public long SaleId { get; set; }
	public string ReceiptNumber { get; set; } = string.Empty;
	public decimal Total { get; set; }
	public decimal? Change { get; set; }
	public int PaymentMethod { get; set; }
	public string? MpesaReference { get; set; }
	public DateTime CreatedAt { get; set; }
	public List<SaleItemLineDto> Items { get; set; } = new();
}

public class DashboardSummaryDto
{
	public long ShiftId { get; set; }
	public string ShiftName { get; set; } = string.Empty;
	public bool ShiftActive { get; set; }
	public DateTime OpenedAt { get; set; }
	public decimal TotalSales { get; set; }
	public decimal CashSales { get; set; }
	public decimal MpesaSales { get; set; }
	public int TransactionCount { get; set; }
	public decimal AverageSale => TransactionCount == 0 ? 0 : Math.Round(TotalSales / TransactionCount, 2);
}

public class CloseShiftRequestDto
{
	public decimal ActualCashCounted { get; set; }
	public string? VarianceReason { get; set; } // required only if there's a difference
}

public class CloseShiftResponseDto
{
	public long ShiftId { get; set; }
	public decimal ExpectedCash { get; set; }
	public decimal ActualCashCounted { get; set; }
	public decimal Difference { get; set; }
	public DateTime ClosedAt { get; set; }
}


public class VehicleTypeDto
{
	public long VehicleTypeId { get; set; }
	public string Name { get; set; } = string.Empty;
}
