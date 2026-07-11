using DataAccessLayer.Common;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityModels.Grleamify
{
	public class CarWashProduct : BaseEntity
	{
		[StringLength(50), Unicode(false)]
		public string Name { get; set; } = string.Empty;      // "Basic Wash", "Vacuum", "Waxing"
		public decimal Price { get; set; }
		public bool IsActive { get; set; } = true;
	}
}