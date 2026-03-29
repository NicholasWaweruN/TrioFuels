
using BussinessLogic.Authentication.CommonTasks;
using BussinessLogic.Setup;
using DataAccessLayer.Common;
using DataAccessLayer.Context;
using DataAccessLayer.EntityModels.Approvals;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;



namespace BussinessLogic.Sales.PriceApproval
{
	public class GasPriceApproval : IGasPriceApproval
	{
		private readonly OTOContext _context;
		private readonly IAuthCommonTasks _authentications;
		private readonly ICommonSetups _setups;
		private readonly IEmailService _emailService;

		public GasPriceApproval(OTOContext context, IAuthCommonTasks authentications, ICommonSetups setups, IEmailService emailService)
		{
			_context = context;
			_authentications = authentications;
			_setups = setups;
			_emailService = emailService;
		}
		/// <summary>
		/// Adds a new gas price approval request.
		/// </summary>
		public async Task<ServiceResponse<object>> AddApprovalAsync(PriceApprovalDto model)
		{
			try
			{
				// Validate current user
				var usercode = _authentications.Usercode();
				if (string.IsNullOrWhiteSpace(usercode))
					return ServiceResponse<object>.Error("Unauthorized access.");

				// Basic model validation
				if (model == null)
					return ServiceResponse<object>.Information("Invalid request data.", null);

				if (string.IsNullOrWhiteSpace(model.NumberPlate) || model.NumberPlate.Length != 7)
					return ServiceResponse<object>.Information("Number plate is required and must be exactly 7 characters.", null);

				if (model.Quantity <= 0)
					return ServiceResponse<object>.Information("Quantity must be greater than zero.", null);

				if (model.ProposedPrice <= 0)
					return ServiceResponse<object>.Information("Proposed price must be greater than zero.", null);

				// Check pending approval
				var pendingApprovalExists = await _context.PriceApproval
					.AnyAsync(p => p.NumberPlate == model.NumberPlate
								   && p.ShiftNumber == model.ShiftNumber
								   && !p.IsApproved);

				if (pendingApprovalExists)
					return ServiceResponse<object>.Information("A pending approval for this number plate and shift already exists.", null);

				// Validate approver exists
				var approverExists = await _context.PriceApprovers
					.AnyAsync(a => a.ApprovalUserCode == model.ApproverUserCode);

				if (!approverExists)
					return ServiceResponse<object>.Information("Invalid approver specified.", null);

				// Generate approval code
				var approvalCode = await _setups.GetCodeGenerator("ApprovalCode");

                // Replace this line:
                // var newRec = new PriceApproval

                // With the fully qualified class name for the PriceApproval entity model.
                // Assuming the class is defined in DataAccessLayer.EntityModels.Approvals namespace:

                var newRec = new DataAccessLayer.EntityModels.Approvals.PriceApproval
                {
                    ApprovalCode = approvalCode,
                    Approver = model.ApproverUserCode,
                    DateCreated = DateTime.UtcNow,
                    Initiator = _authentications.Name() ?? string.Empty,
                    IsApproved = false,
                    NumberPlate = model.NumberPlate,
                    Notes = model.Notes,
                    OriginalPrice = model.OriginalPrice,
                    ProposedPrice = model.ProposedPrice,
                    IsApprovalExecuted = false,
                    Quantity = model.Quantity,
                    ShiftNumber = model.ShiftNumber,
                    UserCode = usercode
                };

				// Send email notification to approver
				var approver = await _context.PriceApprovers
					.FirstOrDefaultAsync(a => a.ApprovalUserCode == model.ApproverUserCode);
				var initiatorName = _authentications.Name() ?? "Unknown User";
				var reviewLink = $"https://yourdomain.com/approvals/review/{approvalCode}"; // Replace with actual link
				var emailBody = BuildApprovalEmailBody(
					approverName: approver?.AppoverName ?? "Approver",
					numberPlate: model.NumberPlate,
					quantity: model.Quantity,
					originalPrice: model.OriginalPrice,
					proposedPrice: model.ProposedPrice,
					notes: model.Notes,
					initiator: initiatorName,
					shiftNumber: model.ShiftNumber,
					reviewLink: reviewLink);

				var recipientString = await (from u in _context.Users
											 where u.UserCode == model.ApproverUserCode
											 select u.Email).FirstOrDefaultAsync();

				string[] recipients = recipientString?
					.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
					?? [];

				recipients.ToList().Add(",cosmasrotich@protoenergy.com"); // Add a default recipient if needed

				var initiatorEmail = await (from u in _context.Users
											where u.UserCode == usercode
											select u.Email).FirstOrDefaultAsync();

				var subject = "New Gas Price Approval Request";
				_emailService.SendEmail(
					toEmail: recipients.ToString() ?? string.Empty,
					toccEmail: initiatorEmail ?? string.Empty,
					subject: subject,
					body: emailBody
				);



				await _context.PriceApproval.AddAsync(newRec);
				await _context.SaveChangesAsync();

				return ServiceResponse<object>.Success("Approval request submitted successfully.", null);
			}
			catch (Exception)
			{
				// log ex with stack trace
				// _logger.LogError(ex, "Error adding price approval.");
				return ServiceResponse<object>.Error("An unexpected error occurred while adding approval.");
			}
		}

		private static string BuildApprovalEmailBody(string approverName,string numberPlate,decimal quantity,
		decimal originalPrice,decimal proposedPrice,string notes,string initiator,string shiftNumber,string reviewLink)
		{
			return $@"
			<html>
			<body style='font-family:Segoe UI, sans-serif; color:#333;'>
				<p>👋 Hello <strong>{approverName}</strong>,</p>

				<p>A new <strong>price approval request</strong> has been submitted and requires your attention ✅.</p>

				<h3>📌 Request Details:</h3>
				<ul>
					<li>🚘 <b>Vehicle Number Plate:</b> {numberPlate}</li>
					<li>📦 <b>Quantity:</b> {quantity} litres</li>
					<li>💵 <b>Original Price:</b> {originalPrice:C}</li>
					<li>💲 <b>Proposed Price:</b> {proposedPrice:C}</li>
					<li>📝 <b>Notes:</b> {notes}</li>
					<li>👤 <b>Initiated By:</b> {initiator}</li>
					<li>🕒 <b>Shift Number:</b> {shiftNumber}</li>
				</ul>

				<p>
					👉 <a href='{reviewLink}' 
						  style='display:inline-block;padding:10px 20px;
								 background-color:#0078D7;color:white;
								 text-decoration:none;border-radius:5px;'>
						Review & Approve
					</a>
				</p>

				<p>🙏 Thank you for keeping things running smoothly! 🌟</p>

				<p>Best regards,<br/>💼 Your System</p>
			</body>
			</html>";
		}


		public async Task<ServiceResponse<List<PendingApprovalDto>>> GetPendingApprovalsAsync()
		{
			try
			{
				var result = await (from a in _context.PriceApproval
									join aps in _context.PriceApprovers
										on a.Approver equals aps.ApprovalUserCode into gj
									from aps in gj.DefaultIfEmpty() // LEFT JOIN
									select new PendingApprovalDto
									{
										ApprovalCode = a.ApprovalCode,
										NumberPlate = a.NumberPlate,
										Quantity = a.Quantity,
										OriginalPrice = a.OriginalPrice,
										ProposedPrice = a.ProposedPrice,
										ApproverName = aps != null ? aps.AppoverName : string.Empty, // handle nulls
										Initiator = a.Initiator,
										IsUsed = a.IsApprovalExecuted == true ? "Used" : "Not Used",
										Status = a.IsApproved == true ? "Approved" : "Not Approved",
									}).ToListAsync();


				return ServiceResponse<List<PendingApprovalDto>>.Success("Success", result);
			}
			catch (Exception ex)
			{
				// Optional logging: _logger.LogError(ex, "Error fetching pending approvals");
				return ServiceResponse<List<PendingApprovalDto>>.Error("Error fetching pending approvals: " + ex.Message);
			}
		}
		// approve
		public async Task<ServiceResponse<object>> AprrovePrice(string approvalCode)
		{
			try
			{
				var userCode = _authentications.Usercode();
				// Check user
				var approverCode = await (from a in _context.PriceApprovers
										  where a.ApprovalUserCode == userCode
										  select a.ApprovalUserCode).FirstOrDefaultAsync();

				if (string.IsNullOrEmpty(approverCode))
					return ServiceResponse<object>.Information("Unauthorized access", null);

				// Find the approval record
				var approval = await _context.PriceApproval
					.FirstOrDefaultAsync(p => p.ApprovalCode == approvalCode);

				if (approval == null)
					return ServiceResponse<object>.Information("Approval request not found.", null);

				if (approval.IsApprovalExecuted)
					return ServiceResponse<object>.Information("This approval request has already been processed.", null);

				// Update approval status
				approval.IsApproved = true;
				approval.IsApprovalExecuted = false;
				approval.Approver = userCode;

				_context.PriceApproval.Update(approval);
				await _context.SaveChangesAsync();

				return ServiceResponse<object>.Success("Approval completed successfully.", null);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error("Error approving price: " + ex.Message);
			}
		}

		//Add price approvers
		public async Task<ServiceResponse<object>> AddPriceApproversAsync(string userCode)
		{
			try
			{
				// Check if the user exists
				var user = await _context.Users
					.FirstOrDefaultAsync(u => u.UserCode == userCode);
				if (user == null)
					return ServiceResponse<object>.Information("User not found.", null);

				// Check if the user is already an approver
				var existingApprover = await _context.PriceApprovers
					.FirstOrDefaultAsync(p => p.ApprovalUserCode == userCode);
				if (existingApprover != null)
					return ServiceResponse<object>.Information("User is already a price approver.", null);

				// Build approver name safely
				var approverName = $"{user.FirstName} {(string.IsNullOrWhiteSpace(user.MiddName) ? "" : user.MiddName + " ")}{user.LastName}".Trim();

				// Ensure createdBy userCode is valid
				var createdBy = _authentications.Usercode();
				if (string.IsNullOrWhiteSpace(createdBy))
					return ServiceResponse<object>.Error("Unable to determine current user.");

				// Create new approver record
				var newApprover = new PriceApprovers
				{
					ApprovalUserCode = user.UserCode,
					AppoverName = approverName,
					DateCreated = DateTime.UtcNow,
					UserCode = createdBy
				};

				await _context.PriceApprovers.AddAsync(newApprover);
				await _context.SaveChangesAsync();

				return ServiceResponse<object>.Success("Price approver added successfully.", null);
			}
			catch (Exception)
			{
				// Log ex internally (stack trace, etc.)
				return ServiceResponse<object>.Error("An unexpected error occurred while adding the price approver.");
			}
		}


		//get all price approvers 
		public async Task<ServiceResponse<object>> ApprovalList()
		{
			try
			{
				var approvers = await (from ap in _context.PriceApprovers
									   where ap.ApprovalUserCode != null
									   select new
									   {
										   ap.ApprovalUserCode,
										   ap.AppoverName,
									   }).ToListAsync();
				if (approvers == null || approvers.Count == 0)
					return ServiceResponse<object>.Information("No approvers found", null);

				return ServiceResponse<object>.Success("Success", approvers);
			}
			catch (Exception ex)
			{
				return ServiceResponse<object>.Error("Error fetching approvers: " + ex.Message);
			}

		}
	}
	public class PendingApprovalDto
	{
		public string ApprovalCode { get; set; } = string.Empty;
		public string NumberPlate { get; set; } = string.Empty;
		public decimal Quantity { get; set; }
		public decimal OriginalPrice { get; set; }
		public decimal ProposedPrice { get; set; }
		public string ApproverName { get; set; } = string.Empty;
		public string Initiator { get; set; } = string.Empty;
		public string Status { get; set; } = string.Empty;
		public string IsUsed { get; set; } = string.Empty;
	}

	public class PriceApprovalDto 
	{
		[Precision(18, 2)]
		public decimal Quantity { get; set; }
		public decimal OriginalPrice { get; set; }
		[Precision(18, 2)]
		public decimal ProposedPrice { get; set; }
		[Required, StringLength(100), Unicode]
		public string NumberPlate { get; set; } = string.Empty;
		[Required, StringLength(10), Unicode]
		public string ApproverUserCode { get; set; } = string.Empty;
		public string Notes { get; set; } = string.Empty;
		[Required, StringLength(20), Unicode]
		public string ShiftNumber { get; set; } = string.Empty;
	}


	
}
