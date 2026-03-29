using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.CustomerService
{
    public class VehicleDto
    {

        [Required, StringLength(8), Unicode(false)]
        public string CustomerCode { get; set; } = string.Empty;
        [Required, StringLength(10), Unicode(false)]
        public string VehicleRegistrationNumber { get; set; } = string.Empty;
        [Required, StringLength(30), Unicode(false)]
        public string VehicleMake { get; set; } = string.Empty;
        [Required, StringLength(30), Unicode(false)]
        public string VehicleModel { get; set; } = string.Empty;
        public int TankCapacity { get; set; }
        [Required, StringLength(2), Unicode(false)]
        public string ProductCode { get; set; } = string.Empty;
		[StringLength(20), Unicode(false)]
		public string PhoneNumber { get; set; } = string.Empty;
		public decimal RoyaltyPointPerLitre { get; set; } = 0m;
	}
    public class UpdateVehicleDto
    {

        [Required, StringLength(10), Unicode(false)]
        public string CustomerCode { get; set; } = string.Empty;
        [Required, StringLength(10), Unicode(false)]
        public string VehicleNumber { get; set; } = string.Empty;
        [Required, StringLength(30), Unicode(false)]
        public string VehicleMake { get; set; } = string.Empty;
        [Required, StringLength(30), Unicode(false)]
        public string VehicleModel { get; set; } = string.Empty;
        public int TankCapacity { get; set; }
        [Required, StringLength(2), Unicode(false)]
        public string ProductCode { get; set; } = string.Empty;
        [Required, StringLength(20), Unicode(false)]
        public string ConversionStation { get; set; } = string.Empty;
        public DateTime ConversionDate { get; set; } 
        public string VehicleCode { get; set; } = string.Empty;
		[StringLength(20), Unicode(false)]
		public string PhoneNumber { get; set; } = string.Empty;
		[StringLength(20), Unicode(false)]
		public string PhoneNumber2 { get; set; } = string.Empty;

	}
    public class ResponseImageModel
    {
        public int StatusID { get; set; }
        public string? ResultMessage { get; set; }
    }
    public class ImageModel
    {
        public string ImageID { get; set; } = string.Empty;
        public string VRegNO { get; set; } = string.Empty;
    }

    public class ChatCompletionChatGPT
    {
        public string Id { get; set; } = string.Empty;
        public string Object { get; set; } = string.Empty;
        public long Created { get; set; }
        public string Model { get; set; } = string.Empty;
        public List<ChoiceChatGPT> Choices { get; set; } = new List<ChoiceChatGPT>();
        public UsageChatGPT Usage { get; set; } = new UsageChatGPT();
        public string SystemFingerprint { get; set; } = string.Empty;
    }
    public class ChoiceChatGPT
    {
        public int Index { get; set; }
        public MessageChatGPT Message { get; set; } = new MessageChatGPT();
        public object Logprobs { get; set; } = string.Empty;
        public string FinishReason { get; set; } = string.Empty;
    }

    public class MessageChatGPT
    {
        public string Role { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }

    public class UsageChatGPT
    {
        public int PromptTokens { get; set; }
        public int CompletionTokens { get; set; }
        public int TotalTokens { get; set; }
    }
    public class ChatCompletion
    {
        public string id { get; set; } = string.Empty;
        public string _object { get; set; } = string.Empty;
        public long reated { get; set; }
        public string model { get; set; } = string.Empty;
        public Choice[]? choices { get; set; }  
        public Usage? usage { get; set; } 
        public string system_fingerprint { get; set; }  = string.Empty;
    }

    public class Choice
    {
        public int index { get; set; }
        public Message? message { get; set; } 
        public object logprobs { get; set; } = new object();
        public string finish_reason { get; set; } = string.Empty;
    }

    public class Message
    {
        public string role { get; set; } = string.Empty;
        public string content { get; set; } = string.Empty;
    }

    public class Usage
    {
        public int prompt_tokens { get; set; }
        public int completion_tokens { get; set; }
        public int total_tokens { get; set; }
    }
	public class CustomerBalanceDto
	{
		public int TotalRecords { get; set; }
		public string CustomerCode { get; set; } = string.Empty;
		public string CustomerName { get; set; } = string.Empty;
		public string VehicleCode { get; set; } = string.Empty;
		public string RegistrationNumber { get; set; } = string.Empty;
		public decimal Balance { get; set; }
		public decimal CreditLimit { get; set; }
	}

	public class VehicleDto2
	{
		public int RowNo { get; set; }
		public string VehicleCode { get; set; } = string.Empty;
		public string VehicleRegistrationNumber { get; set; } = string.Empty;
		public string VehicleModel { get; set; } = string.Empty;
		public string VehicleMake { get; set; } = string.Empty;
		public DateTime? ConversionDate { get; set; }
		public string ConversionStation { get; set; } = string.Empty;
		public string ProductCode { get; set; } = string.Empty;
		public string ProductName { get; set; } = string.Empty;
		 [Precision(18,2)] public decimal TankCapacity { get; set; }
		public bool IsActive { get; set; }
		public string CustomerName { get; set; } = string.Empty;
		public string CustomerCode { get; set; } = string.Empty;
		public string CustomerPhone { get; set; } = string.Empty;
		public string CustomerEmail { get; set; } = string.Empty;
		public DateTime DateCreated { get; set; }
		public string UserCode { get; set; } = string.Empty;
		[Precision(18,2)] public decimal CreditLimit { get; set; } = 0;
		public bool HasTelematic { get; set; } 
		public string? TelematicSerialNumber { get; set; } = string.Empty;

	}
	public class TransferVehicleDto
	{
		public string CustomerCode { get; set; } = string.Empty;
		public string VehicleCode { get; set; } = string.Empty;

	}
}