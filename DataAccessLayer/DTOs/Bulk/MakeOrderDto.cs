using DataAccessLayer.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs.Bulk
{
    public class OrderDto
    {
        [Required]
        public string CustomerLocationId { get; set; } = string.Empty;
        [Required]
        public int OrderQuantity { get; set; } 
        [Required]
        public double Price { get; set; }
        [Required]
        public DateTime ExpectedDeliveryDate { get; set; } 
        [Required]
        public int ProductId { get; set; }
    }
    public class CustomerDto 
    {
        [Required]
        public string CustomerName { get; set; } = string.Empty;
        [Required]
        public string CustomerPhone { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        [Required]
        public string CustomerType { get; set; } = string.Empty;
        public string KRAPIN { get; set; } = string.Empty;
        public string SAPCode { get; set; } = string.Empty;
		public decimal ExpectedQuantity { get; set; }
		public decimal ExpectedQuantityPeriod { get; set; }

	}
    public class CustomerLocationsDto 
    {
        public string CustomerId { get; set; } = string.Empty;
        public string LocationName { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class CustomerListDto
    {
        public string CustomerLocationId { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string CustomerId { get; set; } = string.Empty;
        public string LocationName { get; set; } = string.Empty;
        public string GpsLocation { get; set; } = string.Empty;
    }
    [Keyless] // Mark this entity as keyless
    public class CustomerWithLocation
    {
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public string CustomerId { get; set; } = string.Empty;
        public string KRAPIN { get; set; } = string.Empty;
        public string CustomerType { get; set; } = string.Empty;
        public string LocationName { get; set; } = string.Empty;
        public string CustomerLocationId { get; set; } = string.Empty;
        public string GeoLocation { get; set; } = string.Empty;

        public List<CustomerLocation> Locations { get; set; } = new List<CustomerLocation>(); // List of locations
    }

    public class CustomerLocation
    {
        public string LocationName { get; set; } = string.Empty;
        public string CustomerLocationId { get; set; } = string.Empty;
        public string GeoLocation { get; set; } = string.Empty;
    }

}
