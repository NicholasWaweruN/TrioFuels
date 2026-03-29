using DataAccessLayer.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.EntityModels.Transactions
{
    public class Shift : BaseEntity
    {
        [Required, StringLength(20), Unicode(false)]
        public string ShiftNumber { get; set; } = string.Empty;
        [Required, StringLength(10), Unicode(false)]
        public new string UserCode { get; set; } = string.Empty;
        [Required, StringLength(4), Unicode(false)]
        public string DispenserCode { get; set; } = string.Empty;
        [AllowedValues(1,0,2)]
        public int ShiftStatus { get; set; } 
        public DateTime ShiftStartTime { get; set; } = DateTime.UtcNow;
        public DateTime? ShiftEndTime { get; set; } = DateTime.UtcNow;
        public bool IsEmailSent { get; set; } = false;
		[Required, StringLength(50), Unicode(false)]
		public string EmailConversationId  { get; set; } = string.Empty;
		public bool IsReplySent { get; set;} = false;
	}
}
