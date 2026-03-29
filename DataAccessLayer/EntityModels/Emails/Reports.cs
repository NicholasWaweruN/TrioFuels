using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.EntityModels.Emails
{
	public class Reports
	{
		[Key]
		public string Id { get; set; } = string.Empty;
		public string ReportName { get; set; } = string.Empty;
	}
}