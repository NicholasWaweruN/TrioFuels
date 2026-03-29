using DataAccessLayer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DTOs.Bulk
{
    public class BulkCutsomersDto
    {
        public class CustomerLocationsDto
        {
            public string CustomerId { get; set; } = string.Empty;
            public string CustomerLocationId { get; set; } = string.Empty;
            public string LoactionName { get; set; } = string.Empty;
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }
    }
}
