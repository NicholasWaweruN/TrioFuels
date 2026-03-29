using BusinessLogic.Worker.SalesReport;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace BussinessLogic.Worker.RecordedTotalizer_Readings
{
	public class FraudAuditReport
	{
		private readonly OTOContext _context;
		private readonly ILogger<FraudAuditReport> _logger;
		public FraudAuditReport(OTOContext context,ILogger<FraudAuditReport> logger) 
		{
			_context = context;
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}


		public async Task ShiftAuditReport(Mails mails, string shiftNumber)
		{
			if (mails is null)
				throw new ArgumentException("Recipients not found");

			var connection = _context.Database.GetDbConnection();

						const string detailSql = @"
			SELECT s.ShiftNumber, SaleId, StationName, DispenserName, NozzleName, Attendant_Name, Litres, Amount, SalesDate, Price, Vehicle, ProductName,
				   CustomerName, Transid
			FROM vw_salesData s
			WHERE s.SaleId IN (
				SELECT t.SaleId
				FROM QuantityTransactions t
				JOIN (
					SELECT VehicleCode, ShiftNumber, CAST(RoundedDate AS DATE) AS SaleDate
					FROM QuantityTransactions
					WHERE PaymentTypeCode = 0
					GROUP BY VehicleCode, ShiftNumber, CAST(RoundedDate AS DATE)
					HAVING COUNT(*) > 1
				) dup ON t.VehicleCode = dup.VehicleCode
					   AND t.ShiftNumber = dup.ShiftNumber
					   AND CAST(t.RoundedDate AS DATE) = dup.SaleDate
				JOIN Vehicles v ON t.VehicleCode = v.VehicleCode
				WHERE t.PaymentTypeCode = 0
				  AND v.ProductCode = '01'
				  AND t.ShiftNumber = @ShiftNumber
			)
			AND s.ShiftNumber = @ShiftNumber
			ORDER BY s.Vehicle, s.ShiftNumber, s.SalesDate;
			";

						const string summarySql = @"
			SELECT 
				t.ShiftNumber,
				sd.Attendant_Name,
				sd.StationName,
				sd.DispenserName,
				COUNT(*) AS DuplicateCount
			FROM QuantityTransactions t
			JOIN (
				SELECT VehicleCode, ShiftNumber, CAST(RoundedDate AS DATE) AS SaleDate
				FROM QuantityTransactions
				WHERE PaymentTypeCode = 0
				GROUP BY VehicleCode, ShiftNumber, CAST(RoundedDate AS DATE)
				HAVING COUNT(*) > 1
			) dup ON t.VehicleCode = dup.VehicleCode
				   AND t.ShiftNumber = dup.ShiftNumber
				   AND CAST(t.RoundedDate AS DATE) = dup.SaleDate
			JOIN vw_salesData sd ON sd.SaleId = t.SaleId
			JOIN Vehicles v ON t.VehicleCode = v.VehicleCode
			WHERE t.PaymentTypeCode = 0
			  AND v.ProductCode = '01'
			  AND t.ShiftNumber = @ShiftNumber
			GROUP BY t.ShiftNumber, sd.Attendant_Name, sd.StationName, sd.DispenserName
			ORDER BY sd.Attendant_Name;
			";

			var dataTable = new DataTable();
			var summaries = new List<ShiftSummaryItem>();

			try
			{
				await connection.OpenAsync();

				// Fetch detail data
				using (var detailCmd = connection.CreateCommand())
				{
					detailCmd.CommandText = detailSql;
					detailCmd.CommandType = CommandType.Text;
					var param = detailCmd.CreateParameter();
					param.ParameterName = "@ShiftNumber";
					param.Value = shiftNumber;
					detailCmd.Parameters.Add(param);

					using var reader = await detailCmd.ExecuteReaderAsync();
					dataTable.Load(reader);
				}

				// Fetch summary data
				using (var summaryCmd = connection.CreateCommand())
				{
					summaryCmd.CommandText = summarySql;
					summaryCmd.CommandType = CommandType.Text;
					var param = summaryCmd.CreateParameter();
					param.ParameterName = "@ShiftNumber";
					param.Value = shiftNumber;
					summaryCmd.Parameters.Add(param);

					using var reader = await summaryCmd.ExecuteReaderAsync();
					while (await reader.ReadAsync())
					{
						summaries.Add(new ShiftSummaryItem
						{
							AttendantName = reader.GetString(1),
							StationName = reader.GetString(2),
							DispenserName = reader.GetString(3),
							DuplicateCount = reader.GetInt32(4)
						});
					}
				}

				// Generate Excel and email
				using var excelStream = GenerateExcelFromDataTelematicReport(dataTable);
				var fileName = $"non_compliant_transaction_{shiftNumber}_{DateTime.UtcNow:yyyyMMdd}.xlsx";
				var subject = $"Non-Compliant Transaction Report - Shift {shiftNumber}";
				var summaryHtml = GenerateProfessionalSummaryHtml(shiftNumber, summaries);

				await AuditReportEmailWithSummary(excelStream, fileName, mails, subject, summaryHtml);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error generating or sending Telematic Shift Report with summary.");
			}
			finally
			{
				if (connection.State == ConnectionState.Open)
					await connection.CloseAsync();
			}
		}
		public async Task UngaPromoReport(Mails mails, string shiftNumber)
		{
			if (mails is null)
				throw new ArgumentException("Recipients not found");

			var connection = _context.Database.GetDbConnection();

			const string detailSql = @"
SELECT 
	t.StationName,
	t.PaymentType,
	t.Amount,
	t.Vehicle,
	CASE 
		WHEN t.Amount >= 1000 AND t.Amount <= 1999 THEN '1kg'
		WHEN t.Amount >= 2000 THEN '2kg'
		ELSE NULL
	END AS KgLabel
FROM 
	vw_SalesData t
WHERE  
	t.PaymentType NOT IN ('Voucher', 'Salary')
	AND t.ShiftNumber = @ShiftNumber
	AND (
		(t.Amount >= 1000 AND t.Amount <= 1999)
		OR (t.Amount >= 2000)
	);
";

			const string summarySql = @"
SELECT 
	t.Attendant_Name as AttendantName,
	t.StationName as StationName,
	t.DispenserName,
	CASE 
		WHEN t.Amount >= 1000 AND t.Amount <= 1999 THEN '1kg'
		WHEN t.Amount >= 2000 THEN '2kg'
	END AS UngaPromo,
	COUNT(*) AS TotalCount
FROM 
	vw_SalesData t
WHERE  
	t.PaymentType NOT IN ('Voucher', 'Salary')
	AND t.ShiftNumber = @ShiftNumber
	AND (
		(t.Amount > 1000 AND t.Amount <= 1999)
		OR (t.Amount > 2000)
	)
GROUP BY 
	t.Attendant_Name,
	t.StationName,
	t.DispenserName,
	CASE 
		WHEN t.Amount >= 1000 AND t.Amount <= 1999 THEN '1kg'
		WHEN t.Amount >= 2000 THEN '2kg'
	END;";

			var dataTable = new DataTable();
			var summaries = new List<UngaSummaryItem>();

			try
			{
				await connection.OpenAsync();

				// Fetch detail data
				using (var detailCmd = connection.CreateCommand())
				{
					detailCmd.CommandText = detailSql;
					detailCmd.CommandType = CommandType.Text;

					var param = detailCmd.CreateParameter();
					param.ParameterName = "@ShiftNumber";
					param.Value = shiftNumber;
					detailCmd.Parameters.Add(param);

					using var reader = await detailCmd.ExecuteReaderAsync();
					dataTable.Load(reader);
				}

				// Fetch summary data
				using (var summaryCmd = connection.CreateCommand())
				{
					summaryCmd.CommandText = summarySql;
					summaryCmd.CommandType = CommandType.Text;

					var param = summaryCmd.CreateParameter();
					param.ParameterName = "@ShiftNumber";
					param.Value = shiftNumber;
					summaryCmd.Parameters.Add(param);

					using var reader = await summaryCmd.ExecuteReaderAsync();
					while (await reader.ReadAsync())
					{
						var promoLabel = reader.GetString(3);

						var existing = summaries.FirstOrDefault(s =>
							s.AttendantName == reader.GetString(0) &&
							s.StationName == reader.GetString(1) &&
							s.DispenserName == reader.GetString(2));

						if (existing == null)
						{
							existing = new UngaSummaryItem
							{
								AttendantName = reader.GetString(0),
								StationName = reader.GetString(1),
								DispenserName = reader.GetString(2),
								Unga1Kg = 0,
								Unga2Kg = 0
							};
							summaries.Add(existing);
						}

						if (promoLabel == "1kg")
							existing.Unga1Kg += reader.GetInt32(4);
						else if (promoLabel == "2kg")
							existing.Unga2Kg += reader.GetInt32(4);
					}
				}

				// Generate Excel and email
				using var excelStream = GenerateExcelFromDataTelematicReport(dataTable);
				var fileName = $"unga_promo_{shiftNumber}_{DateTime.UtcNow:yyyyMMdd}.xlsx";
				var subject = $"Unga Promo Report - Shift {shiftNumber}";
				var summaryHtml = UngaPromoProfessionalSummaryHtml(shiftNumber, summaries);

				await AuditReportEmailWithSummary(excelStream, fileName, mails, subject, summaryHtml);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "❌ Error generating or sending Unga Promo Report.");
			}
			finally
			{
				if (connection.State == ConnectionState.Open)
					await connection.CloseAsync();
			}
		}


		public class ShiftSummaryItem
		{
			public string AttendantName { get; set; } = string.Empty;
			public string StationName { get; set; } = string.Empty;
			public string DispenserName { get; set; } = string.Empty;
			public int DuplicateCount { get; set; }
		}

		public class PacketWeights
		{
			public string KgLabel { get; set; } = string.Empty;
			public int TotalCount { get; set; } 
		}
		public class UngaSummaryItem
		{
			public string AttendantName { get; set; } = string.Empty;
			public string StationName { get; set; } = string.Empty;
			public string DispenserName { get; set; } = string.Empty;
			public int Unga1Kg { get; set; } // 1KG Salamary count
			public int Unga2Kg { get; set; } // 2KG Salamary count
		}

		private static string UngaPromoProfessionalSummaryHtml(string shiftNumber, List<UngaSummaryItem> summaries)
		{
			var total1Kg = summaries?.Sum(s => s.Unga1Kg) ?? 0;
			var total2Kg = summaries?.Sum(s => s.Unga2Kg) ?? 0;

			var rowsHtml = new StringBuilder();

			if (summaries != null && summaries.Count > 0)
			{
				foreach (var item in summaries)
				{
					rowsHtml.AppendLine($@"
				<tr>
					<td style='border: 1px solid #ddd; padding: 8px;'>{item.AttendantName}</td>
					<td style='border: 1px solid #ddd; padding: 8px;'>{item.StationName}</td>
					<td style='border: 1px solid #ddd; padding: 8px;'>{item.DispenserName}</td>
					<td style='border: 1px solid #ddd; padding: 8px; text-align: center;'>{item.Unga1Kg}</td>
					<td style='border: 1px solid #ddd; padding: 8px; text-align: center;'>{item.Unga2Kg}</td>
				</tr>");
				}
			}
			else
			{
				rowsHtml.AppendLine(@"
			<tr>
				<td colspan='5' style='border: 1px solid #ddd; padding: 8px; text-align: center; color: #888888;'>
					📭 No Unga Promo sales transactions were found for this shift.
				</td>
			</tr>");
			}

			return $@"
		<div style='font-family: Segoe UI, sans-serif; color: #2c3e50;'>
			<h2 style='color: #1a73e8;'>📦 Unga Promo Summary</h2>

			<p>👋 Hello,</p>

			<p>
				Kindly find below the summary of the <strong>Unga Promo</strong> distribution for 
				<strong>Shift #{shiftNumber}</strong>. This promotion rewards customers with wheat flour based on fuel purchases:
			</p>

			<ul>
				<li>🚗 Fuel between <strong>KES 1,000 – 1,999</strong>: 🎁 1KG packet of wheat flour</li>
				<li>🚚 Fuel of <strong>KES 2,000 and above</strong>: 🎁 2KG packet of wheat flour</li>
			</ul>

			<p>
				<strong>📊 Total Unga Promo Issued:</strong><br/>
				✅ <strong>1KG:</strong> {total1Kg} &nbsp;&nbsp;&nbsp;
				🎯 <strong>2KG:</strong> {total2Kg}
			</p>

			<table style='border-collapse: collapse; width: 100%; margin-top: 20px;'>
				<thead>
					<tr style='background-color: #f8d7da; color: #721c24;'>
						<th style='border: 1px solid #ddd; padding: 10px;'>👨‍🔧 Attendant</th>
						<th style='border: 1px solid #ddd; padding: 10px;'>🏪 Station</th>
						<th style='border: 1px solid #ddd; padding: 10px;'>⛽ Dispenser</th>
						<th style='border: 1px solid #ddd; padding: 10px;'>🥖 1KG</th>
						<th style='border: 1px solid #ddd; padding: 10px;'>🥖 2KG</th>
					</tr>
				</thead>
				<tbody>
					{rowsHtml}
				</tbody>
			</table>

			<p style='margin-top: 25px;'>
				📎 <strong>Kindly find attached the detailed Unga Promo data in Excel format.</strong>
			</p>

			<p style='margin-top: 30px; font-size: 13px; color: #555;'>
				📬 This report was generated automatically by the <strong>Otopay System</strong>.
			</p>
		</div>";
		}


		private static string GenerateProfessionalSummaryHtml(string shiftNumber, List<ShiftSummaryItem> summaries)
		{
			var total = summaries?.Sum(s => s.DuplicateCount) ?? 0;

			var rowsHtml = "";

			if (summaries != null && summaries.Count > 0)
			{
				foreach (var item in summaries)
				{
					rowsHtml += $@"
						<tr>
							<td style='border: 1px solid #ddd; padding: 8px;'>{item.AttendantName}</td>
							<td style='border: 1px solid #ddd; padding: 8px;'>{item.StationName}</td>
							<td style='border: 1px solid #ddd; padding: 8px;'>{item.DispenserName}</td>
							<td style='border: 1px solid #ddd; padding: 8px; text-align: center;'>{item.DuplicateCount}</td>
						</tr>";
										}
									}
									else
									{
										rowsHtml = @"
						<tr>
							<td colspan='4' style='border: 1px solid #ddd; padding: 8px; text-align: center; color: #888888;'>
								No duplicate sales transactions were found for this shift.
							</td>
						</tr>";
									}

									return $@"
						<p><strong>Total Duplicate Sales Records Detected:</strong> {total}</p>

						<table style='border-collapse: collapse; width: 100%; font-family: Arial, sans-serif;'>
							<thead>
								<tr style='background-color: #f8d7da; color: #721c24;'>
									<th style='border: 1px solid #ddd; padding: 8px;'>Attendant Name</th>
									<th style='border: 1px solid #ddd; padding: 8px;'>Station Name</th>
									<th style='border: 1px solid #ddd; padding: 8px;'>Dispenser Name</th>
									<th style='border: 1px solid #ddd; padding: 8px;'>Duplicate Records</th>
								</tr>
							</thead>
							<tbody>
								{rowsHtml}
							</tbody>
						</table>";
		}

		private static async Task AuditReportEmailWithSummary(Stream excelStream, string filename, Mails recipient, string subject, string summaryHtml)
		{
			if (recipient == null)
				throw new ArgumentNullException(nameof(recipient));

			var mail = new MailMessage
			{
				From = new MailAddress("Reports@protoenergy.com"),
				Subject = subject,
				IsBodyHtml = true,
				Body = $@"
					<html>
					<head>
						<style>
							body {{
								font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
								background-color: #ffffff;
								color: #333333;
								line-height: 1.6;
								font-size: 14px;
							}}
							h2 {{
								color: #003366;
								border-bottom: 2px solid #e0e0e0;
								padding-bottom: 6px;
							}}
							p {{
								margin: 10px 0;
							}}
							.note {{
								font-size: 12px;
								color: #888888;
								margin-top: 20px;
							}}
							.footer {{
								margin-top: 30px;
							}}
						</style>
					</head>
					<body>
						<h2>{subject}</h2>

						<p>Dear Security & Operations Team,</p>

						<p>
							Attached is the <strong>System Sales Integrity Report</strong> for <strong>Shift {subject}</strong>.
							This report highlights potential <strong>duplicate or suspicious fueling transactions</strong> identified for further investigation.
						</p>
       						{summaryHtml}
						<p>
							The summary above provides a breakdown of the flagged transactions by <strong>attendant</strong>, <strong>station</strong>, and <strong>dispenser</strong>. 
							Please refer to the attached Excel report for detailed transaction-level data.
						</p>

						<p class='note'>
							<strong>Note:</strong> This is an automated message sent by the otopay audit monitoring program. Please escalate findings as per protocol.
						</p>

						<p class='footer'>
							Regards,<br/>
							<strong>Otopay Monitoring Service</strong><br/>
							Proto Energy Ltd
						</p>
					</body>
					</html>"
			};

			// Add recipients
			if (!string.IsNullOrWhiteSpace(recipient.ToEmails))
			{
				foreach (var email in recipient.ToEmails.Split(',', StringSplitOptions.RemoveEmptyEntries))
				{
					mail.To.Add(email.Trim());
				}
			}

			// Add CCs
			if (!string.IsNullOrWhiteSpace(recipient.CcEmails))
			{
				foreach (var email in recipient.CcEmails.Split(',', StringSplitOptions.RemoveEmptyEntries))
				{
					mail.CC.Add(email.Trim());
				}
			}

			// Attach Excel file
			if (excelStream.CanSeek)
				excelStream.Position = 0;

			mail.Attachments.Add(new Attachment(excelStream, filename, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"));

			// SMTP Configuration
			var credentials = new NetworkCredential("Reports@protoenergy.com", "Tag50274");

			using var smtp = new SmtpClient("smtp.office365.com")
			{
				EnableSsl = true,
				Port = 587,
				Credentials = credentials
			};

			await smtp.SendMailAsync(mail);
		}

		private static MemoryStream GenerateExcelFromDataTelematicReport(DataTable dataTable)
		{
			using var excelEngine = new Syncfusion.XlsIO.ExcelEngine();
			var application = excelEngine.Excel;
			application.DefaultVersion = Syncfusion.XlsIO.ExcelVersion.Excel2016;

			// Create workbook and worksheet
			var workbook = application.Workbooks.Create(1);
			var sheet = workbook.Worksheets[0];

			// Import data
			sheet.ImportDataTable(dataTable, true, 1, 1);

			// Auto-fit columns
			sheet.UsedRange.AutofitColumns();

			// Save to stream
			var stream = new MemoryStream();
			workbook.SaveAs(stream);
			stream.Position = 0;

			return stream;
		}

	}
}