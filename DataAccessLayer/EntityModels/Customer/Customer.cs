using DataAccessLayer.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.EntityModels.Customer
{

	public class Organisations : BaseEntity
	{
		[Required, StringLength(50), Unicode(false)]
		public string OrganisationName { get; set; } = string.Empty;
		[Required, StringLength(20), Unicode(false)]
		public string OrganisationCode { get; set; } = string.Empty;
		[Required, StringLength(20), Unicode(false)]
		public string OrganisationPhone { get; set; } = string.Empty;
		[Required, StringLength(150), Unicode(false)]
		public string OrganisationEmail { get; set; } = string.Empty;
		[Required, StringLength(10), Unicode(false)]
		public OrganisationType OrganisationType { get; set; }  // e.g. "Company", "NGO", etc.
	}
	//OrganisationType entity
    public class OrganisationTypes 
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int OrganisationTypeId { get; set; } 
		[Required, StringLength(50), Unicode(false)]
		public string Description { get; set; } = string.Empty;
	}
	// Customer Entity

	public class Customer : BaseEntity
    {
        [Required, StringLength(50), Unicode(false)]
        public string CustomerName { get; set; } = string.Empty;
        [Required, StringLength(20), Unicode(false)]
        public string CustomerPhone { get; set; } = string.Empty;
        [Required, StringLength(70), Unicode(false)]
        public string CustomerEmail { get; set; } = string.Empty;
		[Required, StringLength(50), Unicode(false)]
		public string? OrganisationCode { get; set; } = string.Empty;
		[Required, StringLength(10), Unicode(false)]
        public string CustomerCode { get; set; } = string.Empty;
        [Required, StringLength(30)]
        public string IdentificationNumber { get; set; } = string.Empty;
        [StringLength(20)]
        public string? KRAPin { get; set; } = string.Empty;
		[Precision(18,2)] public decimal CreditLimit { get; set; }
		public bool Receive_Receipts { get; set; }
		public bool Receive_Statements { get; set; }
		public bool IsCreditCustomer { get; set; } = false;
		[Precision(18,2)]
		public decimal BaseLoyaltyPoints { get; set; } = 0m;
	}

	//enum for TypeOfOrganisation
	public enum OrganisationType
	{
		Individual = 1,
		Corporate = 2,
		NGO = 3,
		Government = 4,
		Sacco = 5,
		Other = 6
	}

	// Vehicle Entity

	public class Vehicle : BaseEntity
    {
		
        [Required, StringLength(20), Unicode(false)]
        public string CustomerCode { get; set; } = string.Empty;
        [Required, StringLength(20), Unicode(false)]
        public string VehicleCode { get; set; } = string.Empty;
        [Required, StringLength(20), Unicode(false)]
        public string VehicleRegistrationNumber { get; set; } = string.Empty;
        [Required, StringLength(50), Unicode(false)]
        public string VehicleMake { get; set; } = string.Empty;
        [Required, StringLength(50), Unicode(false)]
        public string VehicleModel { get; set; } = string.Empty;
        public int TankCapacity { get; set; }
        [Required, StringLength(5), Unicode(false)]
        public string ProductCode { get; set; } = string.Empty;
        [Required, StringLength(50), Unicode(false)]
        public string ConversionStation { get; set; } = string.Empty;
        public DateTime ConversionDate { get; set; }
        public bool IsActive { get; set; }
        [Required, StringLength(50), Unicode(false)]
        public string Status { get; set; } = string.Empty; 
        [Required, StringLength(50), Unicode(false)]
        public string NFC_CardNumber { get; set; } = string.Empty; 
        [Required, StringLength(30), Unicode(false)]
        public string TransactionPIN { get; set; } = string.Empty;
		[StringLength(20), Unicode(false)]
		public string PhoneNumber { get; set; } = string.Empty;
		[StringLength(20), Unicode(false)]
		public string PhoneNumber2 { get; set; } = string.Empty;
		[Precision(18,2)] public decimal CreditLimit { get; set; } = 0;
		[Precision(18, 2)] public decimal Discount { get; set; }
		public string TelematicSerialNumber { get; set; } = string.Empty;
		public bool IsTelematicInstalled  { get; set; } 
		public DateTime TelematicInstallationDate { get; set; } = DateTime.UtcNow;
		public decimal RoyaltyPointPerLitre { get; set; } = 0m;
	}
	public class Customer_Complains : BaseEntity
    {
        [Required, StringLength(10), Unicode(false)]
        public string VehicleCode { get; set; } = string.Empty;
        [Required, StringLength(10), Unicode(false)]
        public string CustomerCode { get; set; } = string.Empty;
        [Required, StringLength(10), Unicode(false)]
        public string ComplainCode { get; set; } = string.Empty;
        [Required, StringLength(30),Unicode(false)]
        public string ComplainDescription { get; set; } = string.Empty;
        public bool IsActive { get; set; }

    }
    public class ComplainsTypes : BaseEntity
    {
        [Required, StringLength(10),Unicode(false)]
        public string ComplainCode { get; set; } = string.Empty;
        [Required, StringLength(40),Unicode(false)]
        public string Description { get; set; } = string.Empty;
    }
    public class VehicleStatusTypes : BaseEntity
    {
        [Required, StringLength(10), Unicode(false)]
        public string StatusCode { get; set; } = string.Empty;
        [Required, StringLength(50), Unicode(false)]
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
    public class TransFeredVehicles : BaseEntity
    {
        [Required, StringLength(20), Unicode(false)]
        public string NewCustomerCode { get; set; } = string.Empty;
        public DateTime TransFerDate { get; set; }
        [Required, StringLength(20), Unicode(false)]
        public string CustomerCode { get; set; } = string.Empty;
        [Required, StringLength(20), Unicode(false)]
        public string VehicleCode { get; set; } = string.Empty;
        [Required, StringLength(20), Unicode(false)]
        public string VehicleRegistrationNumber { get; set; } = string.Empty;
        [Required, StringLength(50), Unicode(false)]
        public string VehicleMake { get; set; } = string.Empty;
        [Required, StringLength(50), Unicode(false)]
        public string VehicleModel { get; set; } = string.Empty;
        public int TankCapacity { get; set; }
        [Required, StringLength(20), Unicode(false)]
        public string ProductCode { get; set; } = string.Empty;
        [Required, StringLength(50), Unicode(false)]
        public string ConversionStation { get; set; } = string.Empty;
        public DateTime ConversionDate { get; set; }
        public bool IsActive { get; set; }
        [Required, StringLength(20), Unicode(false)]
        public string Status { get; set; } = string.Empty;
        [Required, StringLength(50), Unicode(false)]
        public string NFC_CardNumber { get; set; } = string.Empty;
        [Required, StringLength(30), Unicode(false)]
        public string TransactionPIN { get; set; } = string.Empty;
    }
    public class TankSizes 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; }
        public int TankCapacity { get; set; }
    }
	public class Walk_In_Customers : BaseEntity
	{
		[Required, StringLength(20), Unicode(false)]
		public string VehicleRegistrationNumber { get; set; } = string.Empty;
		[Required, StringLength(50), Unicode(false)]
		public string VehicleMake { get; set; } = string.Empty;
		[Required, StringLength(5), Unicode(false)]
		public string ProductCode { get; set; } = string.Empty;
		[StringLength(20), Unicode(false)]
		public string PhoneNumber {  get; set; } = string.Empty;
		[StringLength(20), Unicode(false)]
		public string Name {  get; set; } = string.Empty;
		public bool IsActive { get; set; }
		[StringLength(20), Unicode(false)]
		public string KitType { get; set; } = string.Empty ;

	}

	public class FailedTransactions : BaseEntity
	{
		public string RegNo { get; set; } = string.Empty;
		 [Precision(18,2)] public decimal Amount { get; set; } 
	}
}
