using BussinessLogic.Messaging;
using ClosedXML.Excel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Resend;
using System.Data;

public class EmailService : IEmailService
{
	private readonly IResend _resend;
	private readonly IConfiguration _config;
	private readonly ILogger<EmailService> _logger;

	public EmailService(IResend resend, IConfiguration config, ILogger<EmailService> logger)
	{
		_resend = resend;
		_config = config;
		_logger = logger;
	}

	// ── Simple send ───────────────────────────────────────────────────────────
	public async Task SendEmail(string toEmail, string? ccEmail, string subject, string body)
	{
		if (string.IsNullOrWhiteSpace(toEmail) ||
			string.IsNullOrWhiteSpace(subject) ||
			string.IsNullOrWhiteSpace(body))
			throw new ArgumentException("Email, subject, and body are required.");

		try
		{
			var toList = new EmailAddressList();
			toList.Add(toEmail);

			var message = new EmailMessage
			{
				From = FromAddress(),
				To = toList,
				Subject = subject,
				HtmlBody = body,
			};

			if (!string.IsNullOrWhiteSpace(ccEmail))
			{
				var ccList = new EmailAddressList();
				ccList.Add(ccEmail);
				message.Cc = ccList;
			}


			await _resend.EmailSendAsync(message);
			_logger.LogInformation("Email sent to {To}", toEmail);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error sending email via Resend to {To}", toEmail);
			throw;
		}
	}

	// ── Send with Excel attachment ────────────────────────────────────────────
	public async Task SendEmailWithExcelAttachmentAsync(
		string[] toEmails,
		string[] ccEmails,
		DateTime reportDate,
		string subject,
		string body,
		params DataTable[] tables)
	{
		try
		{
			var filename = $"Report_{reportDate:ddMMyyyy}.xlsx";

			using var excelStream = DataTablesToExcelStream(tables);
			var excelBytes = excelStream.ToArray();

			var toList = new EmailAddressList();
			toList.AddRange((IEnumerable<EmailAddress>)toEmails.Where(e => !string.IsNullOrWhiteSpace(e)));

			var ccList = new EmailAddressList();
			ccList.AddRange((IEnumerable<EmailAddress>)ccEmails.Where(e => !string.IsNullOrWhiteSpace(e)));

			var message = new EmailMessage
			{
				From = FromAddress(),
				To = toList,
				Cc = ccList,
				Subject = subject,
				HtmlBody = body,
				Attachments =
				[
					new()
		{
			Filename    = filename,
			Content     = excelBytes,
			ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
		}
				]
			};

			var response = await _resend.EmailSendAsync(message);

			_logger.LogInformation(
				"Excel sent via Resend ({File}, {Sheets} sheet(s)) to {Count} recipient(s). Id: {Id}",
				filename, tables.Length, toEmails.Length, response.Content);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Failed to send Excel email: {Message}", ex.Message);
			throw;
		}
	}

	// ── Helpers ───────────────────────────────────────────────────────────────
	private string FromAddress()
	{
		var name = _config["ResendSettings:FromName"] ?? "FuelFlow Reports";
		var email = _config["ResendSettings:FromEmail"] ?? throw new InvalidOperationException(
						"ResendSettings:FromEmail is not configured.");
		return $"{name} <{email}>";
	}

	private static MemoryStream DataTablesToExcelStream(DataTable[] tables)
	{
		var workbook = new XLWorkbook();

		foreach (var dt in tables)
		{
			var sheetName = string.IsNullOrWhiteSpace(dt.TableName) ? "Sheet" : dt.TableName;
			var ws = workbook.Worksheets.Add(sheetName);

			// ── Header row ────────────────────────────────────────────────
			for (int col = 0; col < dt.Columns.Count; col++)
			{
				var cell = ws.Cell(1, col + 1);
				cell.Value = dt.Columns[col].ColumnName;
				cell.Style.Font.Bold = true;
				cell.Style.Font.FontColor = XLColor.White;
				cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#1F3864");
				cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
				cell.Style.Border.BottomBorder = XLBorderStyleValues.Medium;
				cell.Style.Border.BottomBorderColor = XLColor.FromHtml("#2E75B6");
			}

			// ── Data rows ─────────────────────────────────────────────────
			for (int row = 0; row < dt.Rows.Count; row++)
			{
				bool isTotal = dt.Rows[row][0]?.ToString() == "TOTAL";
				bool isAltRow = row % 2 != 0;
				XLColor rowBg = isTotal ? XLColor.FromHtml("#FFF2CC")
								 : isAltRow ? XLColor.FromHtml("#F2F2F2")
								 : XLColor.White;

				for (int col = 0; col < dt.Columns.Count; col++)
				{
					var cell = ws.Cell(row + 2, col + 1);
					var value = dt.Rows[row][col];

					cell.Value = value == DBNull.Value || value is null
						? Blank.Value
						: XLCellValue.FromObject(value);

					cell.Style.Fill.BackgroundColor = rowBg;
					cell.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
					cell.Style.Border.BottomBorderColor = XLColor.FromHtml("#D9D9D9");

					if (isTotal)
					{
						cell.Style.Font.Bold = true;
						cell.Style.Font.FontColor = XLColor.FromHtml("#1F3864");
					}

					var colName = dt.Columns[col].ColumnName;

					if (value is decimal or double or float)
					{
						bool isAmount = colName.Contains("Amount", StringComparison.OrdinalIgnoreCase)
									 || colName.Contains("Price", StringComparison.OrdinalIgnoreCase);
						bool isLitres = colName.Contains("Litre", StringComparison.OrdinalIgnoreCase)
									 || colName.Contains("Qty", StringComparison.OrdinalIgnoreCase)
									 || colName.Contains("Quantity", StringComparison.OrdinalIgnoreCase);

						cell.Style.NumberFormat.Format = isAmount || isLitres ? "#,##0.00" : "0.00";
						cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
					}
					else if (value is DateTime)
					{
						cell.Style.NumberFormat.Format = "dd/MM/yyyy";
						cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
					}
					else
					{
						cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
					}
				}
			}

			// ── Freeze + auto-fit ──────────────────────────────────────────
			ws.SheetView.FreezeRows(1);
			ws.Columns().AdjustToContents(minWidth: 10, maxWidth: 40);

			// ── Total row accent border ────────────────────────────────────
			for (int row = 0; row < dt.Rows.Count; row++)
			{
				if (dt.Rows[row][0]?.ToString() != "TOTAL") continue;
				var totalRow = ws.Row(row + 2);
				totalRow.Style.Border.TopBorder = XLBorderStyleValues.Medium;
				totalRow.Style.Border.TopBorderColor = XLColor.FromHtml("#1F3864");
				break;
			}
		}

		var stream = new MemoryStream();
		workbook.SaveAs(stream);
		workbook.Dispose();
		stream.Position = 0;
		return stream;
	}
}