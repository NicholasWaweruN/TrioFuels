using DataAccessLayer.DTOs.Messaging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Data;
using System.Net.Mail;
using Xunit;

// ─────────────────────────────────────────────────────────────
// Interfaces
// ─────────────────────────────────────────────────────────────
public interface ISmtpClient : IDisposable
{
	void Send(MailMessage message);
	Task SendMailAsync(MailMessage message);
}

public interface ISmtpClientFactory
{
	ISmtpClient Create();
}

// ─────────────────────────────────────────────────────────────
// Testable EmailService (mirrors your real one — swap in production)
// ─────────────────────────────────────────────────────────────
public class EmailServiceTestable
{
	private readonly SmtpSettings _smtp;
	private readonly ISmtpClientFactory _factory;
	private readonly ILogger<EmailServiceTestable> _logger;

	public EmailServiceTestable(
		IOptions<SmtpSettings> smtp,
		ISmtpClientFactory factory,
		ILogger<EmailServiceTestable> logger)
	{
		_smtp = smtp.Value;
		_factory = factory;
		_logger = logger;
	}

	public void SendEmail(string toEmail, string? ccEmail, string subject, string body)
	{
		if (string.IsNullOrWhiteSpace(toEmail) ||
			string.IsNullOrWhiteSpace(subject) ||
			string.IsNullOrWhiteSpace(body))
			throw new ArgumentException("Email, subject, and body are required.");

		using var client = _factory.Create();
		using var message = BuildMessage(subject, body);
		message.To.Add(toEmail);
		if (!string.IsNullOrWhiteSpace(ccEmail))
			message.CC.Add(ccEmail);

		client.Send(message);
		_logger.LogInformation("Email sent to {To}", toEmail);
	}

	public async Task SendEmailWithExcelAttachmentAsync(
		string[] toEmails,
		string[] ccEmails,
		DateTime reportDate,
		string subject,
		string body,
		DataTable data)
	{
		await using var csvStream = DataTableToCsvStream(data);
		var filename = $"Report{reportDate:ddMMyyyy}.csv";

		using var client = _factory.Create();
		using var message = BuildMessage(subject, body);
		using var attach = new Attachment(csvStream, filename, "text/csv");

		foreach (var to in toEmails) message.To.Add(to);
		foreach (var cc in ccEmails) message.CC.Add(cc);
		message.Attachments.Add(attach);

		await client.SendMailAsync(message);
		_logger.LogInformation("Attachment email sent ({File})", filename);
	}

	private MailMessage BuildMessage(string subject, string body) => new()
	{
		From = new MailAddress(_smtp.Username, _smtp.DisplayName),
		Subject = subject,
		Body = body,
		IsBodyHtml = true,
	};

	private static MemoryStream DataTableToCsvStream(DataTable table)
	{
		var sb = new System.Text.StringBuilder();
		sb.AppendLine(string.Join(",", table.Columns
			.Cast<DataColumn>().Select(c => CsvEscape(c.ColumnName))));
		foreach (DataRow row in table.Rows)
			sb.AppendLine(string.Join(",", row.ItemArray
				.Select(v => CsvEscape(v?.ToString() ?? ""))));
		return new MemoryStream(System.Text.Encoding.UTF8.GetBytes(sb.ToString()));
	}

	private static string CsvEscape(string value) =>
		(value.Contains(',') || value.Contains('"') || value.Contains('\n'))
			? $"\"{value.Replace("\"", "\"\"")}\""
			: value;
}

// ─────────────────────────────────────────────────────────────
// Helpers
// ─────────────────────────────────────────────────────────────
file static class Helpers
{
	public static IOptions<SmtpSettings> DefaultOptions() =>
		Options.Create(new SmtpSettings
		{
			Host = "smtp.gmail.com",
			Port = 587,
			EnableSsl = true,
			Username = "test@gmail.com",
			Password = "app-password",
			DisplayName = "FuelFlow Reports",
		});

	public static (Mock<ISmtpClient> clientMock, Mock<ISmtpClientFactory> factoryMock)
		MockSmtp()
	{
		var clientMock = new Mock<ISmtpClient>();
		var factoryMock = new Mock<ISmtpClientFactory>();
		factoryMock.Setup(f => f.Create()).Returns(clientMock.Object);
		return (clientMock, factoryMock);
	}

	public static EmailServiceTestable Build(
		Mock<ISmtpClientFactory>? factory = null,
		IOptions<SmtpSettings>? opts = null)
	{
		var (_, fm) = factory is null ? MockSmtp() : (new Mock<ISmtpClient>(), factory);
		return new EmailServiceTestable(
			opts ?? DefaultOptions(),
			fm.Object,
			Mock.Of<ILogger<EmailServiceTestable>>());
	}

	public static DataTable SampleTable()
	{
		var dt = new DataTable();
		dt.Columns.Add("Name");
		dt.Columns.Add("Amount");
		dt.Rows.Add("Alice", "1000");
		dt.Rows.Add("Bob, Jr.", "2,500");
		dt.Rows.Add("Carol \"C\" Smith", "0");
		return dt;
	}

	// Reads attachment stream INSIDE the callback — before MailMessage is disposed.
	public static (Mock<ISmtpClient>, Mock<ISmtpClientFactory>, Func<string>) CsvCapture()
	{
		var (clientMock, factoryMock) = MockSmtp();
		var csv = string.Empty;

		clientMock
			.Setup(c => c.SendMailAsync(It.IsAny<MailMessage>()))
			.Callback<MailMessage>(m =>
			{
				var stream = m.Attachments[0].ContentStream;
				stream.Position = 0;
				csv = new System.IO.StreamReader(stream).ReadToEnd();
			})
			.Returns(Task.CompletedTask);

		return (clientMock, factoryMock, () => csv);
	}

	// Copies primitive fields INSIDE the callback — safe to read after disposal.
	public static (Mock<ISmtpClient>, Mock<ISmtpClientFactory>, Func<CapturedMessage>)
		MessageCapture()
	{
		var (clientMock, factoryMock) = MockSmtp();
		CapturedMessage captured = new();

		clientMock
			.Setup(c => c.SendMailAsync(It.IsAny<MailMessage>()))
			.Callback<MailMessage>(m =>
			{
				captured = new CapturedMessage
				{
					ToAddresses = m.To.Select(a => a.Address).ToList(),
					CcAddresses = m.CC.Select(a => a.Address).ToList(),
					Subject = m.Subject,
					AttachmentName = m.Attachments.FirstOrDefault()
									  ?.ContentDisposition?.FileName ?? "",
					AttachmentType = m.Attachments.FirstOrDefault()
									  ?.ContentType.MediaType ?? "",
				};
			})
			.Returns(Task.CompletedTask);

		return (clientMock, factoryMock, () => captured);
	}
}

file record CapturedMessage
{
	public List<string> ToAddresses { get; init; } = [];
	public List<string> CcAddresses { get; init; } = [];
	public string Subject { get; init; } = "";
	public string AttachmentName { get; init; } = "";
	public string AttachmentType { get; init; } = "";
}

// ─────────────────────────────────────────────────────────────
// Tests — SendEmail (sync)
// ─────────────────────────────────────────────────────────────
public class EmailService_SendEmail
{
	[Fact]
	public void Calls_SmtpClient_Send_once()
	{
		var (clientMock, factoryMock) = Helpers.MockSmtp();
		Helpers.Build(factoryMock)
			   .SendEmail("recipient@example.com", null, "Hello", "Body");

		clientMock.Verify(c => c.Send(It.IsAny<MailMessage>()), Times.Once);
	}

	[Fact]
	public void Sets_correct_To_address()
	{
		MailMessage? captured = null;
		var (clientMock, factoryMock) = Helpers.MockSmtp();
		clientMock.Setup(c => c.Send(It.IsAny<MailMessage>()))
				  .Callback<MailMessage>(m => captured = m);

		Helpers.Build(factoryMock).SendEmail("to@example.com", null, "Subj", "Body");

		Assert.Contains("to@example.com", captured!.To.Select(a => a.Address));
	}

	[Fact]
	public void Adds_CC_when_provided()
	{
		MailMessage? captured = null;
		var (clientMock, factoryMock) = Helpers.MockSmtp();
		clientMock.Setup(c => c.Send(It.IsAny<MailMessage>()))
				  .Callback<MailMessage>(m => captured = m);

		Helpers.Build(factoryMock).SendEmail("to@example.com", "cc@example.com", "Subj", "Body");

		Assert.Contains("cc@example.com", captured!.CC.Select(a => a.Address));
	}

	[Fact]
	public void Does_not_add_CC_when_null()
	{
		MailMessage? captured = null;
		var (clientMock, factoryMock) = Helpers.MockSmtp();
		clientMock.Setup(c => c.Send(It.IsAny<MailMessage>()))
				  .Callback<MailMessage>(m => captured = m);

		Helpers.Build(factoryMock).SendEmail("to@example.com", null, "Subj", "Body");

		Assert.Empty(captured!.CC);
	}

	[Fact]
	public void Does_not_add_CC_when_whitespace()
	{
		MailMessage? captured = null;
		var (clientMock, factoryMock) = Helpers.MockSmtp();
		clientMock.Setup(c => c.Send(It.IsAny<MailMessage>()))
				  .Callback<MailMessage>(m => captured = m);

		Helpers.Build(factoryMock).SendEmail("to@example.com", "   ", "Subj", "Body");

		Assert.Empty(captured!.CC);
	}

	[Fact]
	public void Sets_IsBodyHtml_true()
	{
		MailMessage? captured = null;
		var (clientMock, factoryMock) = Helpers.MockSmtp();
		clientMock.Setup(c => c.Send(It.IsAny<MailMessage>()))
				  .Callback<MailMessage>(m => captured = m);

		Helpers.Build(factoryMock).SendEmail("to@example.com", null, "Subj", "<b>Body</b>");

		Assert.True(captured!.IsBodyHtml);
	}

	[Fact]
	public void Sets_From_to_configured_username_and_display_name()
	{
		MailMessage? captured = null;
		var (clientMock, factoryMock) = Helpers.MockSmtp();
		clientMock.Setup(c => c.Send(It.IsAny<MailMessage>()))
				  .Callback<MailMessage>(m => captured = m);

		Helpers.Build(factoryMock).SendEmail("to@example.com", null, "Subj", "Body");

		Assert.Equal("test@gmail.com", captured!.From!.Address);
		Assert.Equal("FuelFlow Reports", captured.From.DisplayName);
	}

	[Theory]
	[InlineData("", "Subject", "Body")]
	[InlineData("  ", "Subject", "Body")]
	[InlineData("to@x.com", "", "Body")]
	[InlineData("to@x.com", "Sub", "")]
	public void Throws_ArgumentException_for_missing_required_fields(
		string to, string subject, string body)
	{
		Assert.Throws<ArgumentException>(
			() => Helpers.Build().SendEmail(to, null, subject, body));
	}

	[Fact]
	public void Propagates_SmtpException_on_send_failure()
	{
		var (clientMock, factoryMock) = Helpers.MockSmtp();
		clientMock.Setup(c => c.Send(It.IsAny<MailMessage>()))
				  .Throws(new SmtpException("Connection refused"));

		Assert.Throws<SmtpException>(
			() => Helpers.Build(factoryMock).SendEmail("to@example.com", null, "Sub", "Body"));
	}
}

// ─────────────────────────────────────────────────────────────
// Tests — SendWithAttachment (async)
// ─────────────────────────────────────────────────────────────
public class EmailService_SendWithAttachment
{
	[Fact]
	public async Task Calls_SendMailAsync_once()
	{
		var (clientMock, factoryMock) = Helpers.MockSmtp();
		clientMock.Setup(c => c.SendMailAsync(It.IsAny<MailMessage>()))
				  .Returns(Task.CompletedTask);

		await Helpers.Build(factoryMock).SendEmailWithExcelAttachmentAsync(
			["a@example.com"], [], DateTime.Today, "Sub", "Body", Helpers.SampleTable());

		clientMock.Verify(c => c.SendMailAsync(It.IsAny<MailMessage>()), Times.Once);
	}

	[Fact]
	public async Task Filename_contains_formatted_date()
	{
		var (_, factoryMock, getMsg) = Helpers.MessageCapture();

		await Helpers.Build(factoryMock).SendEmailWithExcelAttachmentAsync(
			["a@example.com"], [], new DateTime(2025, 6, 15), "Sub", "Body",
			Helpers.SampleTable());

		Assert.Equal("Report15062025.csv", getMsg().AttachmentName);
	}

	[Fact]
	public async Task Attachment_content_type_is_text_csv()
	{
		var (_, factoryMock, getMsg) = Helpers.MessageCapture();

		await Helpers.Build(factoryMock).SendEmailWithExcelAttachmentAsync(
			["a@example.com"], [], DateTime.Today, "Sub", "Body", Helpers.SampleTable());

		Assert.Equal("text/csv", getMsg().AttachmentType);
	}

	[Fact]
	public async Task All_To_recipients_are_added()
	{
		var (_, factoryMock, getMsg) = Helpers.MessageCapture();
		string[] recipients = ["a@example.com", "b@example.com", "c@example.com"];

		await Helpers.Build(factoryMock).SendEmailWithExcelAttachmentAsync(
			recipients, [], DateTime.Today, "Sub", "Body", Helpers.SampleTable());

		Assert.Equal(3, getMsg().ToAddresses.Count);
		foreach (var r in recipients)
			Assert.Contains(r, getMsg().ToAddresses);
	}

	[Fact]
	public async Task CC_recipients_are_added()
	{
		var (_, factoryMock, getMsg) = Helpers.MessageCapture();

		await Helpers.Build(factoryMock).SendEmailWithExcelAttachmentAsync(
			["a@example.com"], ["mgr@example.com"], DateTime.Today, "Sub", "Body",
			Helpers.SampleTable());

		Assert.Contains("mgr@example.com", getMsg().CcAddresses);
	}

	[Fact]
	public async Task Propagates_exception_on_send_failure()
	{
		var (clientMock, factoryMock) = Helpers.MockSmtp();
		clientMock.Setup(c => c.SendMailAsync(It.IsAny<MailMessage>()))
				  .ThrowsAsync(new SmtpException("Send failed"));

		await Assert.ThrowsAsync<SmtpException>(() =>
			Helpers.Build(factoryMock).SendEmailWithExcelAttachmentAsync(
				["a@example.com"], [], DateTime.Today, "Sub", "Body", Helpers.SampleTable()));
	}
}

// ─────────────────────────────────────────────────────────────
// Tests — CSV generation
// ─────────────────────────────────────────────────────────────
public class EmailService_CsvGeneration
{
	[Fact]
	public async Task Csv_contains_header_row()
	{
		var (_, factoryMock, getCsv) = Helpers.CsvCapture();

		await Helpers.Build(factoryMock).SendEmailWithExcelAttachmentAsync(
			["a@example.com"], [], DateTime.Today, "Sub", "Body", Helpers.SampleTable());

		Assert.StartsWith("Name,Amount", getCsv());
	}

	[Fact]
	public async Task Csv_contains_data_rows()
	{
		var (_, factoryMock, getCsv) = Helpers.CsvCapture();

		await Helpers.Build(factoryMock).SendEmailWithExcelAttachmentAsync(
			["a@example.com"], [], DateTime.Today, "Sub", "Body", Helpers.SampleTable());

		var csv = getCsv();
		Assert.Contains("Alice", csv);
		Assert.Contains("1000", csv);
	}

	[Fact]
	public async Task Csv_escapes_values_containing_commas()
	{
		var (_, factoryMock, getCsv) = Helpers.CsvCapture();

		await Helpers.Build(factoryMock).SendEmailWithExcelAttachmentAsync(
			["a@example.com"], [], DateTime.Today, "Sub", "Body", Helpers.SampleTable());

		Assert.Contains("\"Bob, Jr.\"", getCsv());
	}

	[Fact]
	public async Task Csv_escapes_values_containing_double_quotes()
	{
		var (_, factoryMock, getCsv) = Helpers.CsvCapture();

		await Helpers.Build(factoryMock).SendEmailWithExcelAttachmentAsync(
			["a@example.com"], [], DateTime.Today, "Sub", "Body", Helpers.SampleTable());

		// Carol "C" Smith  →  "Carol ""C"" Smith"
		Assert.Contains("\"Carol \"\"C\"\" Smith\"", getCsv());
	}

	[Fact]
	public async Task Empty_DataTable_produces_header_only()
	{
		var (_, factoryMock, getCsv) = Helpers.CsvCapture();

		var empty = new DataTable();
		empty.Columns.Add("Col1");
		empty.Columns.Add("Col2");

		await Helpers.Build(factoryMock).SendEmailWithExcelAttachmentAsync(
			["a@example.com"], [], DateTime.Today, "Sub", "Body", empty);

		var lines = getCsv().Split('\n', StringSplitOptions.RemoveEmptyEntries);
		Assert.Single(lines);
	}
}