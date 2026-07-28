using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Http;

namespace DataAccessLayer.DTOs.PlateRecognition
{
	
		public class VerifyWalletVehicleRequest
		{
			// Raw base64 string or a data URI, e.g. "data:image/jpeg;base64,/9j/4AAQ..."
			public string Image { get; set; } = string.Empty;
			public string CustomerCode { get; set; } = string.Empty;
		}
	}

