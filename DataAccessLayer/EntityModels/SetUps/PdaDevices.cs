using DataAccessLayer.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityModels.SetUps
{
    public class PdaDevices : BaseEntity
    {
        [Unicode(false), StringLength(10), Required]
        public string DeviceCode { get; set; } = string.Empty;
        [Unicode(false),StringLength(50),Required]
        public string DeviceName { get; set; } = string.Empty;
        [Unicode(false), StringLength(50), Required]
        public string DeviceModel { get; set; } = string.Empty;
        [Unicode(false), StringLength(50), Required]
        public string DeviceSerialNumber { get; set; } = string.Empty;
        [Unicode(false), StringLength(50), Required]
        public string DeviceIMEI { get; set; } = string.Empty;
        [Unicode(false), StringLength(50), Required]
        public string DeviceMacAddress { get; set; } = string.Empty;
        [Unicode(false), StringLength(10), Required]
        public string DispenserCode { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}