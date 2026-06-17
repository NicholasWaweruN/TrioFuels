using BusinessLogic.Sales.CommonSalesTasks;
using BussinessLogic.Authentication.CommonTasks;
using BussinessLogic.Messaging;
using BussinessLogic.PlateDetection;
using BussinessLogic.Setup;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.SetUps;
using DataAccessLayer.EntityModels.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Syncfusion.XlsIO.Implementation.Collections;
using System.Data;
using System.Reflection;
using static BusinessLogic.Services.Services;
using static BussinessLogic.Sales.MissingSales.MisingSale;
using static BussinessLogic.Stock.Stock.StockServicecs;

namespace BussinessLogic.Stock.VarianceReport
{
	public class VarianceReport
	{
		private readonly IAuthCommonTasks _authentication;
		private readonly OTOContext _context;
		private readonly ICommonSetups _setups;
		private readonly IEmailService _emails;
		private readonly ICommonSalesTasks _salesTasks;
		private readonly IEmailWorkflow _workflow;

		public VarianceReport(IAuthCommonTasks authentication, OTOContext context, ICommonSetups setups, IEmailService emails, ICommonSalesTasks salesTasks, IEmailWorkflow workflow)
		{
			_authentication = authentication;
			_context = context;
			_setups = setups;
			_emails = emails;
			_salesTasks = salesTasks;
			_workflow = workflow;
		}


		public async Task<ServiceResponse<object>> GetSalesSummaryReport(string shiftNumber)
		{
			try
			{
				// ─── Fetch & group transactions ────────────────────────────────────────
				var transactions = await (from qt in _context.QuantityTransactions
										  join pt in _context.PaymentTypes on qt.PaymentTypeCode equals pt.PaymentTypeId
										  join n in _context.Nozzles on qt.NozzleCode equals n.NozzleCode
										  join d in _context.Dispensers on qt.DispenserCode equals d.DispenserCode
										  join s in _context.Stations on qt.StationCode equals s.StationCode
										  join u in _context.Users on qt.UserCode equals u.UserCode
										  where qt.ShiftNumber == shiftNumber && !qt.IsReversed
										  select new SalesSummaryRawDto
										  {
											  PaymentTypeCode = qt.PaymentTypeCode,
											  PaymentTypeName = pt.PaymentTypeName,
											  NozzleCode = qt.NozzleCode,
											  NozzleName = n.NozzleName,
											  DispenserCode = qt.DispenserCode,
											  DispenserName = d.DispenserName,
											  StationName = s.StationName,
											  StationCode = qt.StationCode,
											  UserCode = qt.UserCode,
											  Name = string.Join(' ', new[] { u.FirstName, u.MiddName, u.LastName }.Where(x => x != null)),
											  ShiftNumber = qt.ShiftNumber,
											  SaleId = qt.SaleId,
											  VehicleCode = qt.VehicleCode,
											  DateCreated = qt.DateCreated,
											  Price = qt.Price,
											  AmountNet = qt.AmountCredit - qt.AmountDebit,
											  QuantityNet = qt.QuantityCredit - qt.QuantityDebit,
										  }).AsNoTracking().ToListAsync();

				if (transactions.Count == 0)
					return ServiceResponse<object>.Information("No transactions found for the specified shift.", null);

				// ─── Group by PaymentType ──────────────────────────────────────────────
				var grouped = transactions
					.GroupBy(x => new { x.PaymentTypeCode, x.PaymentTypeName })
					.Select(g => new SalesSummaryGroupDto
					{
						PaymentTypeCode = g.Key.PaymentTypeCode,
						PaymentTypeName = g.Key.PaymentTypeName,
						TotalAmount = g.Sum(x => x.AmountNet),
						TotalQuantity = g.Sum(x => x.QuantityNet),
						TransactionCount = g.Count(),
					})
					.OrderBy(x => x.PaymentTypeName)
					.ToList();

				var grandTotalAmount = grouped.Sum(x => x.TotalAmount);
				var grandTotalQuantity = grouped.Sum(x => x.TotalQuantity);
				var first = transactions.First();

				// ─── Build HTML report ─────────────────────────────────────────────────
				var html = BuildSalesSummaryHtml(
					grouped, transactions, first, grandTotalAmount, grandTotalQuantity, shiftNumber);

				return ServiceResponse<object>.Success("Sales summary report generated successfully", html);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error("Something went wrong", ex.Message);
			}
		}

		// ─── HTML Builder ──────────────────────────────────────────────────────────────
		private static string BuildSalesSummaryHtml(
			IEnumerable<SalesSummaryGroupDto> grouped,
			IEnumerable<SalesSummaryRawDto> transactions,
			SalesSummaryRawDto first,
			decimal grandTotalAmount,
			decimal grandTotalQuantity,
			string shiftNumber)
		{
			// ─── Summary rows ──────────────────────────────────────────────────────────
			var summaryRows = string.Join("", grouped.Select(g => $@"
        <tr>
            <td>{g.PaymentTypeName}</td>
            <td class=""num"">{g.TotalQuantity:N3} L</td>
            <td class=""num"">KES {g.TotalAmount:N2}</td>
            <td class=""num"">{g.TransactionCount}</td>
        </tr>"));

			// ─── Detail rows ──────────────────────────────────────────────────────────
			var detailRows = string.Join("", transactions
				.OrderBy(t => t.DateCreated)
				.Select(t => $@"
        <tr>
            <td>{t.DateCreated:HH:mm:ss}</td>
            <td>{t.SaleId}</td>
            <td>{t.NozzleName}</td>
            <td>{t.DispenserName}</td>
            <td>{t.PaymentTypeName}</td>
            <td>{t.VehicleCode ?? "-"}</td>
            <td>{t.Name}</td>
            <td class=""num"">{t.QuantityNet:N3} L</td>
            <td class=""num"">KES {t.Price:N2}</td>
            <td class=""num"">KES {t.AmountNet:N2}</td>
        </tr>"));

			return $@"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
    <title>Sales Summary — Shift {shiftNumber}</title>
    <style>
        *, *::before, *::after {{ box-sizing: border-box; margin: 0; padding: 0; }}

        body {{
            font-family: 'Segoe UI', Arial, sans-serif;
            font-size: 13px;
            color: #1a1a2e;
            background: #f4f6fb;
            padding: 32px 24px;
        }}

        /* ── Header ── */
        .report-header {{
            display: flex;
            justify-content: space-between;
            align-items: flex-start;
            margin-bottom: 28px;
        }}
        .report-header h1 {{
            font-size: 22px;
            font-weight: 700;
            color: #0f3460;
        }}
        .report-header .meta {{
            font-size: 12px;
            color: #555;
            margin-top: 4px;
            line-height: 1.7;
        }}
        .badge {{
            background: #0f3460;
            color: #fff;
            padding: 6px 14px;
            border-radius: 20px;
            font-size: 12px;
            font-weight: 600;
            letter-spacing: 0.5px;
            white-space: nowrap;
        }}

        /* ── KPI Cards ── */
        .kpi-grid {{
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(180px, 1fr));
            gap: 16px;
            margin-bottom: 28px;
        }}
        .kpi-card {{
            background: #fff;
            border-radius: 10px;
            padding: 18px 20px;
            box-shadow: 0 1px 6px rgba(0,0,0,.07);
            border-left: 4px solid #0f3460;
        }}
        .kpi-card.accent {{ border-left-color: #16c79a; }}
        .kpi-card .label {{
            font-size: 11px;
            text-transform: uppercase;
            letter-spacing: 0.6px;
            color: #888;
            margin-bottom: 6px;
        }}
        .kpi-card .value {{
            font-size: 20px;
            font-weight: 700;
            color: #0f3460;
        }}
        .kpi-card.accent .value {{ color: #16c79a; }}

        /* ── Section ── */
        .section {{
            background: #fff;
            border-radius: 10px;
            box-shadow: 0 1px 6px rgba(0,0,0,.07);
            margin-bottom: 24px;
            overflow: hidden;
        }}
        .section-title {{
            padding: 14px 20px;
            font-size: 13px;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: 0.6px;
            background: #0f3460;
            color: #fff;
        }}

        /* ── Tables ── */
        table {{
            width: 100%;
            border-collapse: collapse;
        }}
        thead th {{
            background: #e8edf7;
            color: #0f3460;
            font-size: 11px;
            font-weight: 700;
            text-transform: uppercase;
            letter-spacing: 0.5px;
            padding: 10px 14px;
            text-align: left;
        }}
        thead th.num {{ text-align: right; }}
        tbody td {{
            padding: 9px 14px;
            border-bottom: 1px solid #f0f2f8;
            vertical-align: middle;
        }}
        tbody tr:last-child td {{ border-bottom: none; }}
        tbody tr:hover {{ background: #f7f9ff; }}
        td.num {{ text-align: right; font-variant-numeric: tabular-nums; }}

        /* ── Totals row ── */
        .totals-row td {{
            font-weight: 700;
            background: #e8edf7;
            color: #0f3460;
            border-top: 2px solid #0f3460;
        }}

        /* ── Footer ── */
        .report-footer {{
            text-align: center;
            font-size: 11px;
            color: #aaa;
            margin-top: 24px;
        }}
    </style>
</head>
<body>

    <!-- Header -->
    <div class=""report-header"">
        <div>
            <h1>Sales Summary Report</h1>
            <div class=""meta"">
                <div><strong>Station:</strong> {first.StationName}</div>
                <div><strong>Dispenser:</strong> {first.DispenserName}</div>
                <div><strong>Generated:</strong> {DateTime.UtcNow:MMMM dd, yyyy HH:mm} UTC</div>
            </div>
        </div>
        <span class=""badge"">Shift {shiftNumber}</span>
    </div>

    <!-- KPI Cards -->
    <div class=""kpi-grid"">
        <div class=""kpi-card"">
            <div class=""label"">Total Amount</div>
            <div class=""value"">KES {grandTotalAmount:N2}</div>
        </div>
        <div class=""kpi-card accent"">
            <div class=""label"">Total Litres Sold</div>
            <div class=""value"">{grandTotalQuantity:N3} L</div>
        </div>
        <div class=""kpi-card"">
            <div class=""label"">Payment Methods</div>
            <div class=""value"">{grouped.Count()}</div>
        </div>
        <div class=""kpi-card accent"">
            <div class=""label"">Transactions</div>
            <div class=""value"">{transactions.Count()}</div>
        </div>
    </div>

    <!-- Payment Type Summary -->
    <div class=""section"">
        <div class=""section-title"">Payment Type Breakdown</div>
        <table>
            <thead>
                <tr>
                    <th>Payment Type</th>
                    <th class=""num"">Quantity (L)</th>
                    <th class=""num"">Amount (KES)</th>
                    <th class=""num"">Transactions</th>
                </tr>
            </thead>
            <tbody>
                {summaryRows}
                <tr class=""totals-row"">
                    <td>GRAND TOTAL</td>
                    <td class=""num"">{grandTotalQuantity:N3} L</td>
                    <td class=""num"">KES {grandTotalAmount:N2}</td>
                    <td class=""num"">{transactions.Count()}</td>
                </tr>
            </tbody>
        </table>
    </div>

    <!-- Transaction Detail -->
    <div class=""section"">
        <div class=""section-title"">Transaction Detail</div>
        <table>
            <thead>
                <tr>
                    <th>Time</th>
                    <th>Sale ID</th>
                    <th>Nozzle</th>
                    <th>Dispenser</th>
                    <th>Payment Type</th>
                    <th>Vehicle</th>
                    <th>Attendant</th>
                    <th class=""num"">Qty (L)</th>
                    <th class=""num"">Price/L</th>
                    <th class=""num"">Amount</th>
                </tr>
            </thead>
            <tbody>
                {detailRows}
            </tbody>
        </table>
    </div>

    <div class=""report-footer"">
        Generated by FuelFlow &bull; {DateTime.UtcNow:yyyy} &bull; Shift {shiftNumber}
    </div>

</body>
</html>";
		}

		public class SalesSummaryRawDto
		{
			public int PaymentTypeCode { get; set; }
			public string PaymentTypeName { get; set; } = string.Empty;
			public string NozzleCode { get; set; } = string.Empty;
			public string NozzleName { get; set; } = string.Empty;
			public string DispenserCode { get; set; } = string.Empty;
			public string DispenserName { get; set; } = string.Empty;
			public string StationName { get; set; } = string.Empty;
			public string StationCode { get; set; } = string.Empty;
			public string UserCode { get; set; } = string.Empty;
			public string Name { get; set; } = string.Empty;
			public string ShiftNumber { get; set; } = string.Empty;
			public string SaleId { get; set; } = string.Empty;
			public string? VehicleCode { get; set; }
			public DateTime DateCreated { get; set; }
			public decimal Price { get; set; }
			public decimal AmountNet { get; set; }
			public decimal QuantityNet { get; set; }
		}

		public class SalesSummaryGroupDto
		{
			public int PaymentTypeCode { get; set; } 
			public string PaymentTypeName { get; set; } = string.Empty;
			public decimal TotalAmount { get; set; }
			public decimal TotalQuantity { get; set; }
			public int TransactionCount { get; set; }
		}

		public async Task<ServiceResponse<object>> ClearVariance(string shiftNumber)
		{
			await using var transaction = await _context.Database.BeginTransactionAsync();
			try
			{
				// ─── Load shift context ────────────────────────────────────────────────
				var shift = await _context.Shifts
					.Where(s => s.ShiftNumber == shiftNumber)
					.Select(s => new { s.DispenserCode, s.UserCode })
					.FirstOrDefaultAsync();

				if (shift is null)
					return ServiceResponse<object>.Information("Shift not found.", null);

				var stationCode = await _context.Dispensers
					.Where(d => d.DispenserCode == shift.DispenserCode)
					.Select(d => d.StationCode)
					.FirstOrDefaultAsync();

				// ─── Load variances ────────────────────────────────────────────────────
				var variances = await _context.StockTakeSummaries
					.Where(s => s.ShiftNumber == shiftNumber)
					.ToListAsync();

				if (variances.Count == 0)
					return ServiceResponse<object>.Information("No variances found for this shift.", null);

				// ─── Attempt nozzle swap correction if any nozzle exceeds threshold ───
				var highest = variances.Max(x => Math.Abs(x.ClosingVariance));
				if (highest > 3)
				{
					var transferResult = await NozzleQuantityTransfer(shiftNumber, transaction);
					if (transferResult.ResponseCode == Response.Success)
					{
						// Reload — ReconcileStockSummariesAsync will have updated the summaries
						variances = await _context.StockTakeSummaries
							.Where(s => s.ShiftNumber == shiftNumber)
							.ToListAsync();
					}
				}

				// ─── Re-evaluate total variance after any swap correction ─────────────
				var totalVariance = variances.Sum(x => x.ClosingVariance);

				if (Math.Abs(totalVariance) > 1)
				{
					await transaction.RollbackAsync();
					return ServiceResponse<object>.Information(
						$"Variance of {totalVariance:N3}L exceeds the allowed tolerance. Manual review required.", null);
				}

				// ─── Post compensating transactions for remaining residual ────────────
				foreach (var variance in variances.Where(v => Math.Abs(v.ClosingVariance) > 0))
				{
					var saleId = _setups.GenerateSaleId();

					var quantityTx = new QuantityTransactions
					{
						DateCreated = DateTime.UtcNow,
						UserCode = variance.UserCode ?? string.Empty,
						NozzleCode = variance.NozzleCode,
						QuantityCredit = variance.ClosingVariance,
						QuantityDebit = 0,
						ShiftNumber = shiftNumber,
						SaleId = saleId,
						PaymentTypeCode = 3,                        // system/internal type
						VehicleCode = variance.UserCode ?? string.Empty,
						DispenserCode = shift.DispenserCode,
						AmountCredit = 0,
						AmountDebit = 0,
						IsReversed = false,
						Price = 0,
						StationCode = stationCode ?? string.Empty
					};

					var paymentTx = new PaymentTransactions
					{
						DateCreated = DateTime.UtcNow,
						UserCode = variance.UserCode ?? string.Empty,
						PaymentRefrence = string.Empty,
						SaleId = saleId,
						TransactionAmount = variance.ClosingVariance,
						TransactionAmountDebit = 0,
					};

					variance.VarianceStatus = (int)ShiftStatus.Closed;

					_context.QuantityTransactions.Add(quantityTx);
					_context.PaymentTransactions.Add(paymentTx);
					_context.StockTakeSummaries.Update(variance);
				}

				// ─── Persist & reconcile ───────────────────────────────────────────────
				await _context.SaveChangesAsync();
				await _salesTasks.ReconcileStockSummariesAsync(shiftNumber);
				await transaction.CommitAsync();

				// ─── Audit trail ───────────────────────────────────────────────────────
				var auditMessage =
					$"Variance of {totalVariance:N3}L on shift {shiftNumber} auto-cleared at {DateTime.UtcNow:u}. " +
					$"Falls within the ±1L tolerance bracket.";
				await _authentication.AddUserTrail(auditMessage, nameof(ClearVariance));

				return ServiceResponse<object>.Success("Variance cleared successfully.", null);
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				return ServiceResponse<object>.Error("Failed to clear variance.", ex.Message);
			}
		}
		private async Task<Vehicle> GetVehicleAsync(string vehicleCode)
		{
			return await _context.Vehicles
				.Where(v => v.VehicleCode == vehicleCode)
				.Select(v => new Vehicle
				{
					ProductCode = v.ProductCode,
					VehicleRegistration = v.VehicleRegistrationNumber,
					CreditLimit = v.CreditLimit,
				}).FirstOrDefaultAsync() ?? new Vehicle();
		}

		public async Task<ServiceResponse<object>> NozzleQuantityTransfer(string shiftNumber,IDbContextTransaction? ambientTransaction = null)
		{
			var ownsTransaction = ambientTransaction is null;
			await using var transaction = ownsTransaction
				? await _context.Database.BeginTransactionAsync()
				: null;
			try
			{
				// ─── Step 1: Load per-nozzle variances for this shift ──────────────────
				var summaries = await _context.StockTakeSummaries
					.Where(s => s.ShiftNumber == shiftNumber)
					.ToListAsync();

				if (summaries.Count < 2)
				{
					if (ownsTransaction) await transaction!.RollbackAsync();
					return ServiceResponse<object>.Information("Not enough nozzles to perform transfer.", null);
				}

				// ─── Step 2: Load nozzle → dispenser map ──────────────────────────────
				var nozzleCodes = summaries.Select(s => s.NozzleCode).ToList();

				var nozzleDispensers = await _context.Nozzles
					.Where(n => nozzleCodes.Contains(n.NozzleCode))
					.AsNoTracking()
					.ToDictionaryAsync(n => n.NozzleCode, n => n.DispenserCode);

				// ─── Step 3: Find swap candidate pairs ────────────────────────────────
				// Valid pair: same dispenser, mirrored variances (one large +, one large -)
				// whose net sum is within 10% of the larger magnitude — i.e. they cancel each other.
				var swapPairs = (from over in summaries
								 join under in summaries
									 on nozzleDispensers.GetValueOrDefault(over.NozzleCode)
									 equals nozzleDispensers.GetValueOrDefault(under.NozzleCode)
								 where over.NozzleCode != under.NozzleCode
									&& over.ClosingVariance > 1
									&& under.ClosingVariance < -1
									&& Math.Abs(over.ClosingVariance + under.ClosingVariance)
									   < Math.Max(Math.Abs(over.ClosingVariance),
												  Math.Abs(under.ClosingVariance)) * 0.1m
								 orderby Math.Abs(over.ClosingVariance) descending
								 select new SwapCandidate
								 {
									 DonorNozzleCode = over.NozzleCode,
									 ReceiverNozzleCode = under.NozzleCode,
									 DonorVariance = over.ClosingVariance,
									 ReceiverVariance = under.ClosingVariance,
								 }).ToList();

				if (swapPairs.Count == 0)
				{
					if (ownsTransaction) await transaction!.RollbackAsync();
					return ServiceResponse<object>.Information("No swap candidates found.", null);
				}

				var auditLines = new List<string>();
				var processedPairs = new HashSet<string>();

				foreach (var pair in swapPairs)
				{
					// ─── Deduplicate mirror pairs (N1→N2 and N2→N1 are the same pair) ─
					var pairKey = string.Join("|", new[] { pair.DonorNozzleCode, pair.ReceiverNozzleCode }.Order());
					if (!processedPairs.Add(pairKey)) continue;

					// ─── Step 4: Load donor nozzle transactions ────────────────────────
					var donorTransactions = await _context.QuantityTransactions
						.Where(q => q.ShiftNumber == shiftNumber
								 && q.NozzleCode == pair.DonorNozzleCode
								 && !q.IsReversed)
						.OrderByDescending(q => q.QuantityCredit - q.QuantityDebit)
						.ToListAsync();

					if (donorTransactions.Count == 0) continue;

					// ─── Step 5: Greedy subset selection ──────────────────────────────
					// Target: accumulate enough quantity to cover the receiver's deficit.
					// Stop as soon as we meet or exceed the target — whole transactions only.
					var target = Math.Abs(pair.ReceiverVariance);
					var accumulated = 0m;
					var toMove = new List<QuantityTransactions>();

					foreach (var tx in donorTransactions)
					{
						if (accumulated >= target) break;
						toMove.Add(tx);
						accumulated += tx.QuantityCredit - tx.QuantityDebit;
					}

					if (toMove.Count == 0) continue;

					// ─── Step 6: Reclassify NozzleCode on selected transactions ────────
					var idsToMove = toMove.Select(t => t.Id).ToList();

					await _context.QuantityTransactions
							.Where(q => idsToMove
							.Contains(q.Id)).ExecuteUpdateAsync(q => q
							.SetProperty(x => x.NozzleCode, pair.ReceiverNozzleCode));

					auditLines.Add(
						$"Moved {toMove.Count} tx(s) ({accumulated:N3}L) " +
						$"from nozzle {pair.DonorNozzleCode} → {pair.ReceiverNozzleCode} " +
						$"| donor variance was {pair.DonorVariance:N3}L, " +
						$"receiver variance was {pair.ReceiverVariance:N3}L");
				}

				if (auditLines.Count == 0)
				{
					if (ownsTransaction) await transaction!.RollbackAsync();
					return ServiceResponse<object>.Information("No transactions were moved.", null);
				}

				// ─── Step 7: Reconcile stock summaries after reclassification ──────────
				await _salesTasks.ReconcileStockSummariesAsync(shiftNumber);

				// ─── Step 8: Commit only if we own the transaction ────────────────────
				if (ownsTransaction)
					await transaction!.CommitAsync();

				// ─── Step 9: Audit trail ───────────────────────────────────────────────
				var auditMessage =
					$"Nozzle swap correction applied for shift {shiftNumber} at {DateTime.UtcNow:u}:\n" +
					string.Join("\n", auditLines);

				await _authentication.AddUserTrail(auditMessage, nameof(NozzleQuantityTransfer));

				return ServiceResponse<object>.Success("Nozzle quantity transfer completed.", auditMessage);
			}
			catch (Exception ex)
			{
				if (ownsTransaction)
					await transaction!.RollbackAsync();

				return ServiceResponse<object>.Error("Nozzle transfer failed.", ex.Message);
			}
		}

		// ─── Private DTO ──────────────────────────────────────────────────────────────
		private sealed class SwapCandidate
		{
			public string DonorNozzleCode { get; init; } = string.Empty;
			public string ReceiverNozzleCode { get; init; } = string.Empty;
			public decimal DonorVariance { get; init; }
			public decimal ReceiverVariance { get; init; }
		}

		// ─── DTO ──────────────────── ────────── ────────── ────────── ────────── ──────────
	}
}
