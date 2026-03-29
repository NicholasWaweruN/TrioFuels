using DataAccessLayer.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.EntityModels.Messaging
{
    public class Otps : BaseEntity
    {
        [Required]
        public int OTPType { get; set; }
        [Required, StringLength(300)]
        public string OTPCode { get; set; } = string.Empty;
        public bool OTPStatus { get; set; }
        [Required, StringLength(20)]
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; } = DateTime.UtcNow.AddMinutes(30);
    }
    public class OtpTypes : BaseEntity
    {
        [Required, StringLength(30),Unicode(false)]
        public int OTPType { get; set; }
        [Required, StringLength(100),Unicode(false)]
        public string OTPDescription { get; set; } = string.Empty;
    }

    public class Sms : BaseEntity
    {
        [Required, StringLength(30),Unicode(false)]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required, StringLength(500),Unicode(false)]
        public string Message { get; set; } = string.Empty;
        [Required, StringLength(20),Unicode(false)]
        public string Status { get; set; } = string.Empty;
    }
    public class Emails : BaseEntity
    {
        [StringLength(maximumLength: 1000), Unicode(false), Required]
        public string To { get; set; } = string.Empty;
        [StringLength(maximumLength: 1000), Unicode(false), Required]
        public string ToCC { get; set; } = string.Empty;
        [StringLength(maximumLength: 100), Unicode(false), Required]
        public string From { get; set; } = string.Empty;
        [StringLength(maximumLength: 200), Unicode(false), Required]
        public string ReportCode { get; set; } = string.Empty;
		[StringLength(maximumLength: 40), Unicode(false), Required]
		public string NotificationName { get; set; } = string.Empty;

	}
    public class MessageDetails : BaseEntity
    {
        [StringLength(maximumLength: 100), Unicode(false), Required]
        public string Number { get; set; } = string.Empty;
        [StringLength(maximumLength: 100), Unicode(false), Required]
        public string Status { get; set; } = string.Empty;
        [StringLength(maximumLength: 100), Unicode(false), Required]
        public string MessageId { get; set; } = string.Empty;
        [StringLength(maximumLength: 100), Unicode(false), Required]
        public string Cost { get; set; } = string.Empty;
    }
    public class AfricasTalkingCallback : BaseEntity
    {
        [StringLength(maximumLength: 30), Unicode(false)]
        public string PhoneNumber { get; set; } = string.Empty;
        [StringLength(maximumLength: 50), Unicode(false)]
        public string MessageId { get; set; } = string.Empty;
        [StringLength(maximumLength: 20), Unicode(false)]
        public string Status { get; set; } = string.Empty;
        [StringLength(maximumLength: 20), Unicode(false)]
        public string NetworkCode { get; set; } = string.Empty;
        [StringLength(maximumLength: 100), Unicode(false)]
        public string FailureReason { get; set; } = string.Empty;
         [Precision(18,2)] public decimal Cost { get; set; } = 0;
    }

	public class Messages : BaseEntity
	{
		[Required, StringLength(300),Unicode(false)]
		public string Message { get; set; } = string.Empty; 
		[Required, StringLength(20), Unicode(false)]
		public string PhoneNumber { get; set; } = string.Empty;

	}
	public class SmsCallbacks
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }
		[Required, StringLength(15), Unicode(false)]
		public string PhoneNumber { get; set; } = string.Empty;
		[Required, StringLength(50), Unicode(false)]
		public string MessageId { get; set; } = string.Empty;
		[Required, StringLength(20), Unicode(false)]
		public string Status { get; set; } = string.Empty;
		[Required, StringLength(20), Unicode(false)]
		public string NetworkCode { get; set; } = string.Empty;
		[Required, StringLength(100), Unicode(false)]
		public string FailureReason { get; set; } = string.Empty;
		[Required]
		[Precision(18,2)] 
		public decimal Cost { get; set; } = 0;
		[Required]
		public DateTime DateAdded { get; set; } = DateTime.UtcNow;
	}

	public class RescheduledMessages 
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public long Id { get; set; }
		[Required, StringLength(20), Unicode(false)]
		public string PhoneNumber { get; set; } = string.Empty;
		[Required,StringLength(600),Unicode(false)]
		public string Message { get; set; } = string.Empty;
		public DateTime DateCreated { get; set; } = DateTime.UtcNow;
		public DateTime? DateSent { get; set; }
		public DateTime ScheduledSendingdate { get; set; }
		public bool IsSent { get; set; } = false;

		public string SenderId  { get; set; } = string.Empty;
	}

	public class BulkMessageLog
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		public string Sender { get; set; } = string.Empty;
		public string Message { get; set; } = string.Empty;
		public string RecipientNumber { get; set; } = string.Empty;
		public string Status { get; set; } = string.Empty;
		public string StatusCode { get; set; } = string.Empty;
		public string Cost { get; set; } = string.Empty;
		public string MessageId { get; set; } = string.Empty;
		public string BatchNumber { get; set; } = string.Empty;
		public string DeliveryStatus {  get; set; } = string.Empty;
		public string FailureReason { get; set; } = string.Empty;
		public DateTime Timestamp { get; set; }
	}


}
