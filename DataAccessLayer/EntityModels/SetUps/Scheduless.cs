using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PriceSchedule
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	[Required,StringLength(5),Unicode(false)]
	public string ProductCode { get; set; } = string.Empty;
	[Required,StringLength(5),Unicode(false)]
	public string StationCode { get; set; } = string.Empty; 
	[Required]
	[Column(TypeName = "decimal(6,2)")]
	public decimal ScheduledPrice { get; set; }
	[Required]
	public DateTime StartTime { get; set; }
	[Required]
	public DateTime EndTime { get; set; }
	[Column(TypeName = "decimal(6,2)")]
	public decimal OriginalPrice { get; set; } 
	public bool IsActive { get; set; } = false;
	public bool Processed { get; set; } = false;	
}
