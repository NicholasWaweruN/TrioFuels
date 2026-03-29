using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs.Messaging
{
    public class SmtpSettings
    {
        public string Server { get; set; } = string.Empty;
        public int Port { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool EnableSsl { get; set; }
    }
    public class AfricaIsTalkingSettings
    {
        public string? Username { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public string SMSSenderId { get; set; } = string.Empty;
    }
	public class SafetyAfricasTalking 
	{
		public string? Username { get; set; } = string.Empty;
		public string ApiKey { get; set; } = string.Empty;
		public string SMSSenderId { get; set; } = string.Empty;
	}
	public class ProAfricaIsTalkingSettings
	{
		public string Username { get; set; } = string.Empty;
		public string ApiKey { get; set; } = string.Empty;
		public string SMSSenderId { get; set; } = string.Empty;
	}
	public class SmsCallbackRequest
    {
		[StringLength(20),Unicode,Required]
        public string PhoneNumber { get; set; } = string.Empty;
		[StringLength(100), Unicode, Required]
		public string MessageId { get; set; } = string.Empty;
		[StringLength(20), Unicode, Required]
		public string Status { get; set; } = string.Empty;
		[StringLength(50), Unicode, Required]
		public string NetworkCode { get; set; } = string.Empty;
		[StringLength(70), Unicode, Required]
		public string FailureReason { get; set; } = string.Empty;
         [Precision(18,2)] public decimal Cost { get; set; } = 0;

    }


}
