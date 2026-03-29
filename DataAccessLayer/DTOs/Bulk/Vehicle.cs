using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs.Bulk
{
    public class VehicleDTO
    {
        public string VehicleType { get; set; } = string.Empty;
        public string VehicleModel { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
        public int TankCapacity { get; set; } 
        public int TareWeight { get; set; }
        public string VehicleMake { get; set; } = string.Empty;
    }
    public class UpdateVehicleDTO
    {
        public string VehicleId { get; set; } = string.Empty;
        public string VehicleType { get; set; } = string.Empty;
        public string VehicleModel { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
        public int TankCapacity { get; set; }
        public int TareWeight { get; set; }
        public string VehicleMake { get; set; } = string.Empty;
    }
}
