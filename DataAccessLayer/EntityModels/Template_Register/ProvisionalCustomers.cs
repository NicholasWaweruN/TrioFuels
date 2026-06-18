using DataAccessLayer.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLayer.EntityModels.Template_Register
{
	public class ProvisionalCustomers : BaseEntity
	{
		[StringLength(50),Unicode(false)]
		public string Name { get; set; } = string.Empty;
		[StringLength(20), Unicode(false)]
		public string PhoneNumber { get; set; } = string.Empty;
		[StringLength(10), Unicode(false)]
		public string NumberPlate { get; set; } = string.Empty;
	}
}
