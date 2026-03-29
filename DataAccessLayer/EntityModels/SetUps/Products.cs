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
    public class Products : BaseEntity
    {
        [Required, StringLength(4), Unicode(false)]
        public string ProductCode { get; set; } = string.Empty;
        [Required, StringLength(40), Unicode(false)]
        public string ProductName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
