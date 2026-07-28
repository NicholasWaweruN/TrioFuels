using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Http;

namespace DataAccessLayer.DTOs.PlateRecognition
{
	public class VerifyWalletVehicleRequest
	{
		public IFormFile Image { get; set; } = null!;
		public string CustomerCode { get; set; } = string.Empty;
	}
}
