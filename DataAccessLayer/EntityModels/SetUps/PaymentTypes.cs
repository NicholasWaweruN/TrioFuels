using DataAccessLayer.Common;
using Microsoft.EntityFrameworkCore;
using System.CodeDom.Compiler;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.EntityModels.Transactions
{
    public class PaymentType : BaseEntity
    {
        [Required, StringLength(50), Unicode(false)]
        public string PaymentTypeName { get; set; } = string.Empty;
        [Required, StringLength(2), Unicode(false)]
        public int PaymentTypeId { get; set; } 
        public bool IsAppUsed { get; set; }
        [StringLength(10), Unicode(false)]
        public string ProcessType { get; set; } = string.Empty;
		public bool HasValue { get; set; } = true;
	}
}


