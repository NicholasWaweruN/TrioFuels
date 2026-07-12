using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.Db_Views;
using DataAccessLayer.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;
using static BussinessLogic.Reports.AllReports;

namespace BussinessLogic.Reports
{

	public interface IAllReports
	{ 
		Task<ServiceResponse<ShiftReconciliationResult>> GetShiftReconciliation(string shiftNumber, string? stationCode = null);
		Task<ServiceResponse<SalesByPaymentTypeResult>> GetSalesByPaymentType(string? stationCode, DateTime? startDate = null, DateTime? endDate = null);
		Task<ServiceResponse<ShiftSummaryResult>> GetShiftSummary(string shiftNumber, string? stationCode = null);
		Task<ServiceResponse<MpesaUnusedCodesResult>> GetMpesaUnusedCodesByShift(string shiftNumber, string? tillNumber = null);
		Task<ServiceResponse<CreditGivenResult>> GetCreditGiven(string? stationCode = null, string? customerCode = null, DateTime? startDate = null, DateTime? endDate = null);
		Task<ServiceResponse<CreditAgingResult>> GetCreditAging(string? stationCode = null);
		Task<ServiceResponse<CreditRepaymentsResult>> GetCreditRepayments(string? stationCode = null, string? customerCode = null, DateTime? startDate = null, DateTime? endDate = null);
		Task<ServiceResponse<StockReconciliationResult>> GetStockReconciliation(string shiftNumber, string? stationCode = null);
	}
	public class AllReports : IAllReports
	{
		private readonly OTOContext _context;
		public AllReports(OTOContext context)
		{
			_context = context;
		}

		public async Task<ServiceResponse<MpesaUnusedCodesResult>> GetMpesaUnusedCodesByShift(
	string shiftNumber,
	string? tillNumber = null)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(shiftNumber))
					return ServiceResponse<MpesaUnusedCodesResult>.Error("Shift number is required", null);

				var query = _context.MpesaTransactions.AsNoTracking()
					.Where(m => m.ShiftNumber == shiftNumber)
					.Where(m => m.Status == 1)      // only successful transactions carry a real usable balance
					.Where(m => m.UsageBalance > 0); // exclude fully-used codes

				if (!string.IsNullOrEmpty(tillNumber))
					query = query.Where(m => m.TillNumber == tillNumber);

				var rows = await query
					.OrderByDescending(m => m.TransTime)
					.ToListAsync();

				var mapped = rows.Select(m => new MpesaUnusedCodeDto
				{
					TransID = m.TransID,
					MpesaReceiptNumber = m.MpesaReceiptNumber,
					TillNumber = m.TillNumber,
					TillName = m.TillName,
					PaymentMethod = m.PaymentMethod,
					TransAmount = m.TransAmount,
					UsageBalance = m.UsageBalance,
					AmountUsed = m.TransAmount - m.UsageBalance,
					UsageStatus = m.UsageBalance == m.TransAmount ? 1 : 2, // 1=not used, 2=partially used
					CustomerName = $"{m.FirstName} {m.MiddName} {m.LastName}".Trim(),
					MSISDN = m.MSISDN,
					TransTime = m.TransTime,
					ShiftNumber = m.ShiftNumber
				}).ToList();

				var result = new MpesaUnusedCodesResult
				{
					ShiftNumber = shiftNumber,
					Codes = mapped,
					NotUsedCount = mapped.Count(x => x.UsageStatus == 1),
					PartiallyUsedCount = mapped.Count(x => x.UsageStatus == 2),
					TotalUnusedAmount = mapped.Sum(x => x.UsageBalance),
					TotalTransactionAmount = mapped.Sum(x => x.TransAmount)
				};

				if (mapped.Count == 0)
					return ServiceResponse<MpesaUnusedCodesResult>.Information("No unused M-Pesa codes found for this shift", result);

				return ServiceResponse<MpesaUnusedCodesResult>.Success("Mpesa Unused Codes Retrieved", result);
			}
			catch (Exception ex)
			{
				return ServiceResponse<MpesaUnusedCodesResult>.Error($"An error occurred while fetching unused Mpesa codes: {ex.Message}", null);
			}
		}
		public async Task<ServiceResponse<ShiftReconciliationResult>> GetShiftReconciliation(string shiftNumber, string? stationCode = null)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(shiftNumber))
					return ServiceResponse<ShiftReconciliationResult>.Error("Shift number is required", null);

				// Totalizer side: StockTakeSummary joined to Nozzle/Dispenser/Station for names
				var totalizerQuery =
					from stk in _context.StockTakeSummaries.AsNoTracking()
					join n in _context.Nozzles on stk.NozzleCode equals n.NozzleCode
					join pp in _context.PetroleumProducts on n.PetroleumCode equals pp.PetroleumCode
					join p in _context.Prices on pp.PetroleumCode equals p.ProductCode
					join d in _context.Dispensers on n.DispenserCode equals d.DispenserCode
					join s in _context.Stations on d.StationCode equals s.StationCode
					where stk.ShiftNumber == shiftNumber
					select new
					{
						stk.NozzleCode,
						n.NozzleName,
						d.DispenserName,
						s.StationName,
						s.StationCode,
						ProductName = pp.PetroleumName, 
						stk.OpeningReading,
						stk.ClosingReading,
						stk.VarianceStatus,
						PricePerLitre = p.Amount,
						ExpectedLitres = stk.ClosingReading - stk.OpeningReading,
						ExpectedAmount = (stk.ClosingReading - stk.OpeningReading) * p.Amount
					};

				if (!string.IsNullOrEmpty(stationCode))
					totalizerQuery = totalizerQuery.Where(t => t.StationCode == stationCode);

				var totalizerRows = await totalizerQuery.ToListAsync();

				if (totalizerRows.Count == 0)
					return ServiceResponse<ShiftReconciliationResult>.Information("No totalizer records found for this shift", null);

				// Actual sales side: FuelSale view for the same shift, excluding reversed rows
				var salesRows = await _context.Set<FuelSale>()
					.AsNoTracking()
					.Where(f => f.ShiftNumber == shiftNumber && !f.IsReversed)
					.ToListAsync();

				var nozzleResults = new List<NozzleReconciliationDto>();

				foreach (var t in totalizerRows)
				{
					var totalizerDifference = t.ClosingReading - t.OpeningReading;
					var expectedAmount = totalizerDifference * t.PricePerLitre;

					// Match actual sales to this nozzle by name (scoped by station name to reduce collision risk)
					var matchedSales = salesRows
						.Where(f => f.NozzleName == t.NozzleName && f.StationName == t.StationName)
						.ToList();

					var byPaymentType = matchedSales
						.GroupBy(f => f.PaymentType ?? "Unknown")
						.Select(g => new PaymentTypeSalesDto
						{
							PaymentType = g.Key,
							Litres = g.Sum(x => x.Litres),
							Amount = g.Sum(x => x.Amount)
						})
						.OrderBy(p => p.PaymentType)
						.ToList();

					var totalActualLitres = byPaymentType.Sum(p => p.Litres);
					var totalActualAmount = byPaymentType.Sum(p => p.Amount);

					nozzleResults.Add(new NozzleReconciliationDto
					{
						NozzleCode = t.NozzleCode,
						NozzleName = t.NozzleName,
						DispenserName = t.DispenserName,
						StationName = t.StationName,
						ProductName = t.ProductName,
						PricePerLitre = t.PricePerLitre,
						OpeningReading = t.OpeningReading,
						ClosingReading = t.ClosingReading,
						TotalizerDifference = totalizerDifference,
						ExpectedAmount = expectedAmount,
						ActualSalesByPaymentType = byPaymentType,
						TotalActualLitres = totalActualLitres,
						TotalActualAmount = totalActualAmount,
						VarianceLitres = totalActualLitres - totalizerDifference,
						VarianceAmount = totalActualAmount - expectedAmount,
						VarianceStatus = t.VarianceStatus
					});
				}

				var result = new ShiftReconciliationResult
				{
					ShiftNumber = shiftNumber,
					Nozzles = nozzleResults.OrderBy(n => n.StationName).ThenBy(n => n.NozzleName).ToList(),
					TotalTotalizerLitres = nozzleResults.Sum(n => n.TotalizerDifference),
					TotalExpectedAmount = nozzleResults.Sum(n => n.ExpectedAmount),
					TotalActualLitres = nozzleResults.Sum(n => n.TotalActualLitres),
					TotalActualAmount = nozzleResults.Sum(n => n.TotalActualAmount),
					TotalVarianceLitres = nozzleResults.Sum(n => n.VarianceLitres),
					TotalVarianceAmount = nozzleResults.Sum(n => n.VarianceAmount)
				};

				return ServiceResponse<ShiftReconciliationResult>.Success("Shift Reconciliation Retrieved", result);
			}
			catch (Exception ex)
			{
				return ServiceResponse<ShiftReconciliationResult>.Error($"An error occurred while fetching shift reconciliation: {ex.Message}", null);
			}
		}


		public async Task<ServiceResponse<CreditAgingResult>> GetCreditAging(string? stationCode = null)
		{
			try
			{
				var query = _context.CreditTransactions.AsNoTracking().AsQueryable();

				if (!string.IsNullOrEmpty(stationCode))
					query = query.Where(c => c.StationCode == stationCode);

				var all = await query.ToListAsync();

				var customerCodes = all.Select(c => c.CustomerCode).Distinct().ToList();
				var customers = await _context.Customers.AsNoTracking()
					.Where(cu => customerCodes.Contains(cu.CustomerCode))
					.ToDictionaryAsync(cu => cu.CustomerCode);

				var byCustomer = all
					.GroupBy(c => c.CustomerCode)
					.Select(g => new
					{
						CustomerCode = g.Key,
						Balance = g.Sum(x => x.Credit) - g.Sum(x => x.Debit),
						OldestUnpaidDate = g.Where(x => x.Credit > 0).Min(x => (DateTime?)x.DateCreated)
					})
					.Where(x => x.Balance > 0) // only customers who still owe money
					.ToList();

				var now = EatTime.Now;

				var agingRows = byCustomer.Select(c =>
				{
					var daysOutstanding = c.OldestUnpaidDate.HasValue
						? (now - c.OldestUnpaidDate.Value).Days
						: 0;

					customers.TryGetValue(c.CustomerCode, out var custEntity);
					var creditLimit = custEntity?.CreditLimit ?? 0;
					var remainingLimit = creditLimit - c.Balance;

					return new CreditAgingRowDto
					{
						CustomerCode = c.CustomerCode,
						CustomerName = custEntity?.CustomerName ?? "Unknown",
						OutstandingBalance = c.Balance,
						CreditLimit = creditLimit,
						RemainingLimit = remainingLimit,
						AtLimit = remainingLimit <= 0,
						OldestUnpaidDate = c.OldestUnpaidDate,
						DaysOutstanding = daysOutstanding,
						AgeBucket = daysOutstanding <= 30 ? "0-30 days"
								  : daysOutstanding <= 60 ? "31-60 days"
								  : daysOutstanding <= 90 ? "61-90 days"
								  : "90+ days"
					};
				})
				.OrderByDescending(c => c.DaysOutstanding)
				.ToList();

				var result = new CreditAgingResult
				{
					Rows = agingRows,
					TotalOutstanding = agingRows.Sum(r => r.OutstandingBalance),
					CustomersWithBalance = agingRows.Count,
					CustomersAtLimit = agingRows.Count(r => r.AtLimit),
					BucketTotals = agingRows
						.GroupBy(r => r.AgeBucket)
						.Select(g => new AgeBucketTotalDto { Bucket = g.Key, Total = g.Sum(x => x.OutstandingBalance), Count = g.Count() })
						.ToList()
				};

				if (agingRows.Count == 0)
					return ServiceResponse<CreditAgingResult>.Information("No outstanding credit balances", result);

				return ServiceResponse<CreditAgingResult>.Success("Credit Aging Retrieved", result);
			}
			catch (Exception ex)
			{
				return ServiceResponse<CreditAgingResult>.Error($"An error occurred while fetching credit aging: {ex.Message}", null);
			}
		}
		public async Task<ServiceResponse<SalesByPaymentTypeResult>> GetSalesByPaymentType(string? stationCode, DateTime? startDate = null, DateTime? endDate = null)
		{
			try
			{
				if (!startDate.HasValue && !endDate.HasValue)
				{
					var currentDate = EatTime.Now;
					startDate = currentDate.AddDays(-3);
					endDate = currentDate;
				}

				var query = _context.Set<FuelSale>().AsNoTracking()
					.Where(f => f.StationName != null && !f.StationName.Contains("TEST"))
					.Where(f => !f.IsReversed)
					.Where(f => f.SalesDate >= startDate!.Value && f.SalesDate <= endDate!.Value);

				if (!string.IsNullOrEmpty(stationCode))
					query = query.Where(f => f.StationName == stationCode); // adjust if FuelSale exposes StationCode instead

				var rows = await query.ToListAsync();

				var byType = rows
					.GroupBy(f => f.PaymentType ?? "Unknown")
					.Select(g => new PaymentTypeBreakdownDto
					{
						PaymentType = g.Key,
						Litres = g.Sum(x => x.Litres),
						Amount = g.Sum(x => x.Amount),
						TransactionCount = g.Count(),
						PercentOfTotalAmount = 0 // set below once grand total known
					})
					.OrderByDescending(x => x.Amount)
					.ToList();

				var grandTotalAmount = byType.Sum(x => x.Amount);
				foreach (var b in byType)
					b.PercentOfTotalAmount = grandTotalAmount != 0 ? Math.Round(b.Amount / grandTotalAmount * 100, 2) : 0;

				var result = new SalesByPaymentTypeResult
				{
					StartDate = startDate!.Value,
					EndDate = endDate!.Value,
					Breakdown = byType,
					TotalLitres = byType.Sum(x => x.Litres),
					TotalAmount = grandTotalAmount,
					TotalTransactions = byType.Sum(x => x.TransactionCount)
				};

				if (rows.Count == 0)
					return ServiceResponse<SalesByPaymentTypeResult>.Information("No Sales Found", result);

				return ServiceResponse<SalesByPaymentTypeResult>.Success("Sales By Payment Type Retrieved", result);
			}
			catch (Exception ex)
			{
				return ServiceResponse<SalesByPaymentTypeResult>.Error($"An error occurred while fetching sales by payment type: {ex.Message}", null);
			}
		}

		public async Task<ServiceResponse<ShiftSummaryResult>> GetShiftSummary(string shiftNumber, string? stationCode = null)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(shiftNumber))
					return ServiceResponse<ShiftSummaryResult>.Error("Shift number is required", null);

				var query = _context.Set<FuelSale>().AsNoTracking()
					.Where(f => f.ShiftNumber == shiftNumber && !f.IsReversed)
					.Where(f => f.StationName != null && !f.StationName.Contains("TEST"));

				if (!string.IsNullOrEmpty(stationCode))
					query = query.Where(f => f.StationName == stationCode); // adjust if FuelSale exposes StationCode

				var rows = await query.ToListAsync();

				if (rows.Count == 0)
					return ServiceResponse<ShiftSummaryResult>.Information("No sales found for this shift", null);

				var byStation = rows
					.GroupBy(f => f.StationName ?? "Unknown")
					.Select(g => new StationShiftTotalDto
					{
						StationName = g.Key,
						Litres = g.Sum(x => x.Litres),
						Amount = g.Sum(x => x.Amount),
						TransactionCount = g.Count()
					})
					.OrderBy(s => s.StationName)
					.ToList();

				var byPaymentType = rows
					.GroupBy(f => f.PaymentType ?? "Unknown")
					.Select(g => new PaymentTypeBreakdownDto
					{
						PaymentType = g.Key,
						Litres = g.Sum(x => x.Litres),
						Amount = g.Sum(x => x.Amount),
						TransactionCount = g.Count()
					})
					.OrderByDescending(p => p.Amount)
					.ToList();

				var byAttendant = rows
					.GroupBy(f => f.AttendantName)
					.Select(g => new AttendantShiftTotalDto
					{
						AttendantName = g.Key,
						Litres = g.Sum(x => x.Litres),
						Amount = g.Sum(x => x.Amount),
						TransactionCount = g.Count()
					})
					.OrderBy(a => a.AttendantName)
					.ToList();

				var result = new ShiftSummaryResult
				{
					ShiftNumber = shiftNumber,
					ByStation = byStation,
					ByPaymentType = byPaymentType,
					ByAttendant = byAttendant,
					TotalLitres = rows.Sum(r => r.Litres),
					TotalAmount = rows.Sum(r => r.Amount),
					TotalTransactions = rows.Count,
					ReversedTransactionsExcluded = true
				};

				return ServiceResponse<ShiftSummaryResult>.Success("Shift Summary Retrieved", result);
			}
			catch (Exception ex)
			{
				return ServiceResponse<ShiftSummaryResult>.Error($"An error occurred while fetching shift summary: {ex.Message}", null);
			}
		}

		public async Task<ServiceResponse<CreditGivenResult>> GetCreditGiven(string? stationCode = null, string? customerCode = null, DateTime? startDate = null, DateTime? endDate = null)
		{
			try
			{
				if (!startDate.HasValue && !endDate.HasValue)
				{
					var currentDate = EatTime.Now;
					startDate = currentDate.AddDays(-3);
					endDate = currentDate;
				}

				var query =
					from c in _context.CreditTransactions.AsNoTracking()
					join s in _context.Stations on c.StationCode equals s.StationCode
					join cu in _context.Customers on c.CustomerCode equals cu.CustomerCode
					where c.Credit > 0
						  && c.DateCreated >= startDate!.Value && c.DateCreated <= endDate!.Value
					select new CreditGivenRowDto
					{
						CustomerCode = c.CustomerCode,
						CustomerName = cu.CustomerName,
						StationCode = c.StationCode,
						StationName = s.StationName,
						VehicleCode = c.VehicleCode,
						SaleId = c.SaleId,
						TransactionReference = c.TransactionReference,
						Amount = c.Credit,
						DateCreated = c.DateCreated
					};

				if (!string.IsNullOrEmpty(stationCode))
					query = query.Where(c => c.StationCode == stationCode);

				if (!string.IsNullOrEmpty(customerCode))
					query = query.Where(c => c.CustomerCode == customerCode);

				var rows = await query.OrderByDescending(c => c.DateCreated).ToListAsync();

				var result = new CreditGivenResult
				{
					StartDate = startDate!.Value,
					EndDate = endDate!.Value,
					Rows = rows,
					TotalCreditGiven = rows.Sum(r => r.Amount),
					TransactionCount = rows.Count
				};

				if (rows.Count == 0)
					return ServiceResponse<CreditGivenResult>.Information("No credit issued in this period", result);

				return ServiceResponse<CreditGivenResult>.Success("Credit Given Retrieved", result);
			}
			catch (Exception ex)
			{
				return ServiceResponse<CreditGivenResult>.Error($"An error occurred while fetching credit given: {ex.Message}", null);
			}
		}

		public async Task<ServiceResponse<CreditRepaymentsResult>> GetCreditRepayments(
	string? stationCode = null, string? customerCode = null, DateTime? startDate = null, DateTime? endDate = null)
		{
			try
			{
				if (!startDate.HasValue && !endDate.HasValue)
				{
					var currentDate = EatTime.Now;
					startDate = currentDate.AddDays(-3);
					endDate = currentDate;
				}

				var query =
					from c in _context.CreditTransactions.AsNoTracking()
					join s in _context.Stations on c.StationCode equals s.StationCode
					join cu in _context.Customers on c.CustomerCode equals cu.CustomerCode
					where c.Debit > 0
						  && c.DateCreated >= startDate!.Value && c.DateCreated <= endDate!.Value
					select new CreditRepaymentRowDto
					{
						CustomerCode = c.CustomerCode,
						CustomerName = cu.CustomerName,
						StationCode = c.StationCode,
						StationName = s.StationName,
						VehicleCode = c.VehicleCode,
						SaleId = c.SaleId,
						TransactionReference = c.TransactionReference,
						Amount = c.Debit,
						DateCreated = c.DateCreated
					};

				if (!string.IsNullOrEmpty(stationCode))
					query = query.Where(c => c.StationCode == stationCode);

				if (!string.IsNullOrEmpty(customerCode))
					query = query.Where(c => c.CustomerCode == customerCode);

				var rows = await query.OrderByDescending(c => c.DateCreated).ToListAsync();

				var result = new CreditRepaymentsResult
				{
					StartDate = startDate!.Value,
					EndDate = endDate!.Value,
					Rows = rows,
					TotalRepaid = rows.Sum(r => r.Amount),
					TransactionCount = rows.Count
				};

				if (rows.Count == 0)
					return ServiceResponse<CreditRepaymentsResult>.Information("No credit repayments in this period", result);

				return ServiceResponse<CreditRepaymentsResult>.Success("Credit Repayments Retrieved", result);
			}
			catch (Exception ex)
			{
				return ServiceResponse<CreditRepaymentsResult>.Error($"An error occurred while fetching credit repayments: {ex.Message}", null);
			}
		}

		public async Task<ServiceResponse<StockReconciliationResult>> GetStockReconciliation(string shiftNumber, string? stationCode = null)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(shiftNumber))
					return ServiceResponse<StockReconciliationResult>.Error("Shift number is required", null);

				var stockQuery =
					from stk in _context.StockTakeSummaries.AsNoTracking()
					join n in _context.Nozzles on stk.NozzleCode equals n.NozzleCode
					join d in _context.Dispensers on n.DispenserCode equals d.DispenserCode
					join s in _context.Stations on d.StationCode equals s.StationCode
					where stk.ShiftNumber == shiftNumber
					select new
					{
						stk.NozzleCode,
						NozzleName = n.NozzleName,
						StationName = s.StationName,
						StationCode = s.StationCode,
						stk.QuantitySold,
						stk.VarianceStatus
					};

				if (!string.IsNullOrEmpty(stationCode))
					stockQuery = stockQuery.Where(t => t.StationCode == stationCode);

				var stockRows = await stockQuery.ToListAsync();

				if (stockRows.Count == 0)
					return ServiceResponse<StockReconciliationResult>.Information("No stock take records found for this shift", null);

				var salesRows = await _context.Set<FuelSale>()
					.AsNoTracking()
					.Where(f => f.ShiftNumber == shiftNumber && !f.IsReversed)
					.ToListAsync();

				var nozzleResults = new List<NozzleCashReconciliationDto>();

				foreach (var t in stockRows)
				{
					var matchedSales = salesRows
						.Where(f => f.NozzleName == t.NozzleName && f.StationName == t.StationName)
						.ToList();

					var avgPrice = matchedSales.Any() ? matchedSales.Average(f => f.Price) : 0;
					var expectedMoney = t.QuantitySold * avgPrice;

					var byPaymentType = matchedSales
						.GroupBy(f => f.PaymentType ?? "Unknown")
						.Select(g => new PaymentTypeSalesDto
						{
							PaymentType = g.Key,
							Litres = g.Sum(x => x.Litres),
							Amount = g.Sum(x => x.Amount)
						})
						.OrderBy(p => p.PaymentType)
						.ToList();

					var actualCash = byPaymentType.Where(p => p.PaymentType == "Cash").Sum(p => p.Amount);
					var nonCashActual = byPaymentType.Where(p => p.PaymentType != "Cash").Sum(p => p.Amount);
					var expectedCash = expectedMoney - nonCashActual;

					nozzleResults.Add(new NozzleCashReconciliationDto
					{
						NozzleCode = t.NozzleCode,
						NozzleName = t.NozzleName,
						StationName = t.StationName,
						QuantitySold = t.QuantitySold,
						AvgPrice = Math.Round(avgPrice, 2),
						ExpectedMoney = expectedMoney,
						ActualByPaymentType = byPaymentType,
						TotalActualMoney = byPaymentType.Sum(p => p.Amount),
						NonCashActual = nonCashActual,
						ExpectedCash = expectedCash,
						ActualCash = actualCash,
						CashVariance = actualCash - expectedCash,
						VarianceStatus = t.VarianceStatus
					});
				}

				var result = new StockReconciliationResult
				{
					ShiftNumber = shiftNumber,
					Nozzles = nozzleResults.OrderBy(n => n.StationName).ThenBy(n => n.NozzleName).ToList(),
					TotalExpectedMoney = nozzleResults.Sum(n => n.ExpectedMoney),
					TotalActualMoney = nozzleResults.Sum(n => n.TotalActualMoney),
					TotalExpectedCash = nozzleResults.Sum(n => n.ExpectedCash),
					TotalActualCash = nozzleResults.Sum(n => n.ActualCash),
					TotalCashVariance = nozzleResults.Sum(n => n.CashVariance)
				};

				return ServiceResponse<StockReconciliationResult>.Success("Stock Reconciliation Retrieved", result);
			}
			catch (Exception ex)
			{
				return ServiceResponse<StockReconciliationResult>.Error($"An error occurred while fetching stock reconciliation: {ex.Message}", null);
			}
		}
		public class PaymentTypeSalesDto
		{
			public string PaymentType { get; set; } = string.Empty;
			public decimal Litres { get; set; }
			public decimal Amount { get; set; }
		}

		public class NozzleCashReconciliationDto
		{
			public string NozzleCode { get; set; } = string.Empty;
			public string NozzleName { get; set; } = string.Empty;
			public string StationName { get; set; } = string.Empty;

			public decimal QuantitySold { get; set; }
			public decimal AvgPrice { get; set; }
			public decimal ExpectedMoney { get; set; }        // QuantitySold * AvgPrice

			public List<PaymentTypeSalesDto> ActualByPaymentType { get; set; } = new();
			public decimal TotalActualMoney { get; set; }

			public decimal NonCashActual { get; set; }         // sum of all non-Cash payment types
			public decimal ExpectedCash { get; set; }          // ExpectedMoney - NonCashActual
			public decimal ActualCash { get; set; }            // recorded Cash sales
			public decimal CashVariance { get; set; }           // ActualCash - ExpectedCash

			public int VarianceStatus { get; set; }
		}

		public class StockReconciliationResult
		{
			public string ShiftNumber { get; set; } = string.Empty;
			public List<NozzleCashReconciliationDto> Nozzles { get; set; } = new();

			public decimal TotalExpectedMoney { get; set; }
			public decimal TotalActualMoney { get; set; }
			public decimal TotalExpectedCash { get; set; }
			public decimal TotalActualCash { get; set; }
			public decimal TotalCashVariance { get; set; }
		}
		public class NozzleReconciliationDto
		{
			public string NozzleCode { get; set; } = string.Empty;
			public string NozzleName { get; set; } = string.Empty;
			public string DispenserName { get; set; } = string.Empty;
			public string StationName { get; set; } = string.Empty;

			public decimal OpeningReading { get; set; }
			public decimal ClosingReading { get; set; }
			public decimal TotalizerDifference { get; set; }   // Closing - Opening = expected sales (litres)

			public List<PaymentTypeSalesDto> ActualSalesByPaymentType { get; set; } = new();
			public decimal TotalActualLitres { get; set; }
			public decimal TotalActualAmount { get; set; }
			public decimal VarianceLitres { get; set; }         // ActualLitres - TotalizerDifference
			public int VarianceStatus { get; set; }
			public decimal PricePerLitre { get; set; }
			public string? ProductName { get; set; }
			public decimal ExpectedAmount { get; set; }
			public decimal VarianceAmount { get; set; }
		}

		public class ShiftReconciliationResult
		{
			public string ShiftNumber { get; set; } = string.Empty;
			public List<NozzleReconciliationDto> Nozzles { get; set; } = new();

			public decimal TotalTotalizerLitres { get; set; }
			public decimal TotalActualLitres { get; set; }
			public decimal TotalActualAmount { get; set; }
			public decimal TotalVarianceLitres { get; set; }
			public decimal TotalExpectedAmount { get; set; }
			public decimal TotalVarianceAmount { get; set; }
		}
		public class PaymentTypeBreakdownDto
		{
			public string PaymentType { get; set; } = string.Empty;
			public decimal Litres { get; set; }
			public decimal Amount { get; set; }
			public int TransactionCount { get; set; }
			public decimal PercentOfTotalAmount { get; set; }
		}

		public class SalesByPaymentTypeResult
		{
			public DateTime StartDate { get; set; }
			public DateTime EndDate { get; set; }
			public List<PaymentTypeBreakdownDto> Breakdown { get; set; } = new();
			public decimal TotalLitres { get; set; }
			public decimal TotalAmount { get; set; }
			public int TotalTransactions { get; set; }
		}

		public class StationShiftTotalDto
		{
			public string StationName { get; set; } = string.Empty;
			public decimal Litres { get; set; }
			public decimal Amount { get; set; }
			public int TransactionCount { get; set; }
		}

		public class AttendantShiftTotalDto
		{
			public string AttendantName { get; set; } = string.Empty;
			public decimal Litres { get; set; }
			public decimal Amount { get; set; }
			public int TransactionCount { get; set; }
		}

		public class ShiftSummaryResult
		{
			public string ShiftNumber { get; set; } = string.Empty;
			public List<StationShiftTotalDto> ByStation { get; set; } = new();
			public List<PaymentTypeBreakdownDto> ByPaymentType { get; set; } = new();
			public List<AttendantShiftTotalDto> ByAttendant { get; set; } = new();
			public decimal TotalLitres { get; set; }
			public decimal TotalAmount { get; set; }
			public int TotalTransactions { get; set; }
			public bool ReversedTransactionsExcluded { get; set; }
		}
		public class MpesaUnusedCodesResult
		{
			public string ShiftNumber { get; set; } = string.Empty;
			public List<MpesaUnusedCodeDto> Codes { get; set; } = new();
			public int NotUsedCount { get; set; }
			public int PartiallyUsedCount { get; set; }
			public decimal TotalUnusedAmount { get; set; }
			public decimal TotalTransactionAmount { get; set; }
		}
		public class MpesaUnusedCodeDto
		{
			public string TransID { get; set; } = string.Empty;
			public string MpesaReceiptNumber { get; set; } = string.Empty;
			public string TillNumber { get; set; } = string.Empty;
			public string TillName { get; set; } = string.Empty;
			public string PaymentMethod { get; set; } = string.Empty;
			public decimal TransAmount { get; set; }
			public decimal UsageBalance { get; set; }
			public decimal AmountUsed { get; set; }
			public int UsageStatus { get; set; } // 1=not used, 2=partially used
			public string CustomerName { get; set; } = string.Empty;
			public string MSISDN { get; set; } = string.Empty;
			public DateTime TransTime { get; set; }
			public string ShiftNumber { get; set; } = string.Empty;
		}
		public class CreditGivenRowDto
		{
			public string CustomerCode { get; set; } = string.Empty;
			public string CustomerName { get; set; } = string.Empty;
			public string StationCode { get; set; } = string.Empty;
			public string StationName { get; set; } = string.Empty;
			public string VehicleCode { get; set; } = string.Empty;
			public string SaleId { get; set; } = string.Empty;
			public string TransactionReference { get; set; } = string.Empty;
			public decimal Amount { get; set; }
			public DateTime DateCreated { get; set; }
		}

		public class CreditAgingRowDto
		{
			public string CustomerCode { get; set; } = string.Empty;
			public string CustomerName { get; set; } = string.Empty;
			public decimal OutstandingBalance { get; set; }
			public decimal CreditLimit { get; set; }
			public decimal RemainingLimit { get; set; }
			public bool AtLimit { get; set; }
			public DateTime? OldestUnpaidDate { get; set; }
			public int DaysOutstanding { get; set; }
			public string AgeBucket { get; set; } = string.Empty;
		}

		public class CreditAgingResult
		{
			public List<CreditAgingRowDto> Rows { get; set; } = new();
			public decimal TotalOutstanding { get; set; }
			public int CustomersWithBalance { get; set; }
			public int CustomersAtLimit { get; set; }
			public List<AgeBucketTotalDto> BucketTotals { get; set; } = new();
		}



		public class CreditGivenResult
		{
			public DateTime StartDate { get; set; }
			public DateTime EndDate { get; set; }
			public List<CreditGivenRowDto> Rows { get; set; } = new();
			public decimal TotalCreditGiven { get; set; }
			public int TransactionCount { get; set; }
		}

		public class CreditRepaymentRowDto
		{
			public string CustomerCode { get; set; } = string.Empty;
			public string CustomerName { get; set; } = string.Empty;
			public string StationCode { get; set; } = string.Empty;
			public string StationName { get; set; } = string.Empty;
			public string VehicleCode { get; set; } = string.Empty;
			public string SaleId { get; set; } = string.Empty;
			public string TransactionReference { get; set; } = string.Empty;
			public decimal Amount { get; set; }
			public DateTime DateCreated { get; set; }
		}

		public class CreditRepaymentsResult
		{
			public DateTime StartDate { get; set; }
			public DateTime EndDate { get; set; }
			public List<CreditRepaymentRowDto> Rows { get; set; } = new();
			public decimal TotalRepaid { get; set; }
			public int TransactionCount { get; set; }
		}


		public class AgeBucketTotalDto
		{
			public string Bucket { get; set; } = string.Empty;
			public decimal Total { get; set; }
			public int Count { get; set; }
		}

	
	}
}

