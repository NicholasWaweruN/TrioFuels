using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.DTOs.Shifts.Station
{
    public class AddStationDto
    {
        [Required, StringLength(50), Unicode(false)]
        public string StationName { get; set; } = string.Empty;
        [Required, StringLength(50), Unicode(false)]
        public string StationAddress { get; set; } = string.Empty;
        [Required]
        public double StationLatitude { get; set; }
        [Required]
        public double StationLongitude { get; set;}
    }
    public class AddTankDto
    {
        [Required, StringLength(50), Unicode(false)]
        public string TankName { get; set; } = string.Empty;
        [Required, StringLength(10), Unicode(false)]
        public string StationCode { get; set; } = string.Empty;
    }
    public class UpdateTankDto
    {
        [Required, StringLength(20), Unicode(false)]
        public string TankName { get; set; } = string.Empty;
        [Required, StringLength(5), Unicode(false)]
        public string TankCode { get; set; } = string.Empty;
    }
    public class AcceptDeliveryDto
    {
        [Required]
        public string StationCode { get; set; } = string.Empty;
        [Required]
        public string OrderId { get; set; } = string.Empty;
        [Required]
        public double DeliveredQuantity { get; set; }
        [Required]
        public string ReadingBeforeImage { get; set; } = string.Empty;
        public int ReadingBeforeInPerc { get; set; }
        [Required]
        public string ReadingAfterImage { get; set; } = string.Empty;
        public int ReadingAfterInPerc { get; set; } = 0;

    }
    public class AddDispenserDto
    {
        [Required, StringLength(50), Unicode(false)]
        public string DispenserName { get; set; } = string.Empty;
        [Required, StringLength(10), Unicode(false)]
        public string StationCode { get; set; } = string.Empty;
		public string? TillNumber { get; set; }
		[Required]
		public string StorageLocation { get; set; } = string.Empty;
		[Precision(18, 2)]
		public decimal Price { get; set; } = 0m;
		[Required, StringLength(10), Unicode(false)]
		public string ProductCode { get; set; } = string.Empty;
    }
    public class AddNozzleDto
    {
        [Required, StringLength(50), Unicode(false)]
        public string NozzleName { get; set; } = string.Empty;
        [Required, StringLength(10), Unicode(false)]
        public string DispenserCode { get; set; } = string.Empty;
    }
    //UPDATE

    public class updateTankDto
    {
        [Required, StringLength(50), Unicode(false)]
        public string TankName { get; set; } = string.Empty;
        [Required, StringLength(50), Unicode(false)]
        public string TankCode { get; set; } = string.Empty;

    }
    public class UpdateStationDto
    {
        [Required, StringLength(50), Unicode(false)]
        public string StationName { get; set; } = string.Empty;
        [Required, StringLength(50), Unicode(false)]
        public string StationCode { get; set; } = string.Empty;
        [Required, StringLength(50), Unicode(false)]
        public string StationAddress { get; set; } = string.Empty;
    }
    public class UpdateDispenserDto
    {
        [Required, StringLength(50), Unicode(false)]
        public string DispenserName { get; set; } = string.Empty;
        [Required, StringLength(10), Unicode(false)]
        public string DispenserCode { get; set; } = string.Empty;
    }
    public class UpdateNozzleDto
    {
        [Required, StringLength(50), Unicode(false)]
        public string NozzleName { get; set; } = string.Empty;
        [Required, StringLength(10), Unicode(false)]
        public string NozzleCode { get; set; } = string.Empty;
    }
}
