
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace DataAccessLayer.Common
{
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long Id { get; set; } 
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        [StringLength(20), Unicode(false)]
        public string UserCode { get; set; } = string.Empty;
    }
}
