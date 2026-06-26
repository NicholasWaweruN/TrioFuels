using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.DTOs.Customer
{
	using DataAccessLayer.EntityModels.Customer;
	using System.ComponentModel.DataAnnotations;

	public class CustomerDTO
	{
		public string CustomerName { get; set; } = string.Empty;

		private string _customerPhone = string.Empty;

	
		[RegularExpression(@"^((\+2547\d{8})|(07\d{8})|(\+2541\d{8})|(01\d{8}))$",ErrorMessage = "Enter a valid Kenyan phone number (e.g., 0712345678 or +254712345678).")]
		public string CustomerPhone { get => _customerPhone; set => _customerPhone = value?.Trim() ?? string.Empty; }

		[EmailAddress]
		public string CustomerEmail { get; set; } = string.Empty;

		public OrganisationType OrganisationType { get; set; } 
		public string OrganisationCode { get; set; } = string.Empty;

		private string _krapin = string.Empty;
		public string Krapin { get => _krapin;set => _krapin = value?.Trim().ToUpper() ?? string.Empty;}
		public string IdentificationNumber { get; set; } = string.Empty;
		public bool IsCreditCustomer { get; set; } = false;
		public decimal CreditLimit { get; set; } = 0m;
	}

	//Register Organisations
	public class RegisterOrganisationDTO
	{
		[Required]
		public string OrganisationName { get; set; } = string.Empty;
		[Required]
		public OrganisationType OrganisationType { get; set; }
		[Required]
		public string PhoneNumber { get;set; } = string.Empty;
		[Required, EmailAddress]
		public string EmailAddress { get; set; } = string.Empty;

	}

}