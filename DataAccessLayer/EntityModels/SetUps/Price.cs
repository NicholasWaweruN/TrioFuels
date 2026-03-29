using DataAccessLayer.Common;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.EntityModels.Transactions
{
    public class Price : BaseEntity
    {
        [Required, StringLength(10), Unicode(false)]
        public string ProductCode { get; set; } = string.Empty;
        [Precision(18,2)] public decimal Amount { get; set; } = 0;
        [Required, StringLength(10), Unicode(false)]
        public string StationCode { get; set; } = string.Empty;
		[Required, StringLength(10), Unicode(false)]
		public string DispenserCode { get; set; } = string.Empty;
		[Required]
		public decimal Discount { get; set; } = 0; 

	}

	public class AdjustPriceDto
	{
		[Required]
		public string ProductCode { get; set; } = string.Empty;

		[Required]
		public string StationCode { get; set; } = string.Empty;

		[Required]
		[Precision(18, 2)]
		public decimal AdjustmentAmount { get; set; } // can be +ve or -ve
	}


	public class UserAndPrice 
    {
         [Precision(18,2)] public decimal Price { get; set; }
        public string UserCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
	public class PriceList
	{
		public string ProductCode { get; set; } = string.Empty;
		public string StationCode { get; set; } = string.Empty;
		 [Precision(18,2)] public decimal Price { get; set; }
	}
}

