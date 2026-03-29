using BusinessLogic.CustomerService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs.Bulk
{
    public class Deliverydto
    {
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerLocation { get; set; } = string.Empty;
         [Precision(18,2)] public decimal DeliveredQuantity { get; set; }
        public DateTime DeliverlyDate { get; set; }
    }

    public class DeliveryPlandto
    {
        public string UserName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string DeliveryPlanId { get; set; } = string.Empty;
        public string DeliveryPlanStatusName { get; set; } = string.Empty;
        public int DeliveryPlanStatusId { get; set; }
        public int LoadedQuantity { get; set; }
        public int DeliveredQuantity { get; set; }
        public int OffloadedQuantity { get; set; }
        public List<Deliverydto> RecentDelivery { get; set; } = new List<Deliverydto>();
    }
    public class LoadOrderDto
    {
        [Required]
        public string DeliveryPlanId { get; set; } = string.Empty;
        [Required]
        public int QuantityLoaded { get; set; }
        [Required]
        public string LoadingOrder { get; set; } = string.Empty;

    }
    public class BulkOrderDto
    {
        public string OrderId { get; set; } = string.Empty;
        public List<int> QuantityLoaded { get; set; } = new List<int>();
    }
    public class AttachOrderDto
    {
        public string DeliveryPlanId { get; set; } = string.Empty;
        public List<string> OrderIds { get; set; } = new List<string>();
    }
    public class OrderDeliveryDto
    {
        public string OrderId { get; set; } = string.Empty;
        public int QuantityDelivered { get; set; }
		public string DeliveryNoteNo { get; set; } = string.Empty ;
        public string DeliveryNote { get; set; } = string.Empty;
    }
    public class DeliveryInfoDto
    {
        public string UserName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public int DeliveryStatusId { get; set; }
        public string DeliveryPlanId { get; set; } = string.Empty;
        public string StatusName { get; set; } = string.Empty;
        public string PlateNumber { get; set; } = string.Empty;
        public int LoadedQuantity { get; set; }
        public int DeliveredQuantity { get; set; }
        public int OffLoadedQuantity { get; set; }
        public List<OrderInfo> OrderInfo { get; set; } = new List<OrderInfo>();
    }
    public class OrderInfo
    {
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string LocationName { get; set; } = string.Empty;
        public DateTime? DeliveryDate { get; set; }
        public int DeliveredQuantity { get; set; }
    }
    public class DeliveryPlanDTO
    {
        public string DeliveryPlanId { get; set; } = string.Empty;
        public string VehicleRegistrationNumber { get; set; } = string.Empty;
        public string DriverName { get; set; } = string.Empty;
        public string DriverId { get; set; } = string.Empty;
        public string VehicleId { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public int DeliveryStatusId { get; set; } = 0;
        public string DeliveryStatusName { get; set; } = string.Empty;
         [Precision(18,2)] public decimal LoadedQuantity { get; set; } = 0;
         [Precision(18,2)] public decimal OffLoadedQuantity { get; set; } = 0;
        public DateTime? DateLoaded { get; set; }
        public DateTime? DateOffLoaded { get; set; }
        public List<OrderDTO> Orders { get; set; } = new List<OrderDTO>();
    }

    public class OrderDTO
    {
        public string OrderId { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string LocationName { get; set; } = string.Empty;
         [Precision(18,2)] public decimal OrderedQuantity { get; set; }
    }
}
