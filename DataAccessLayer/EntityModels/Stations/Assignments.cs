using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityModels.Stations
{
    public class DispenserAssignment
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Required, StringLength(10), Unicode(false)]
        public string DispenserCode { get; set; } = string.Empty;
        [Required, StringLength(10), Unicode(false)]
        public string StationCode { get; set; } = string.Empty;
        [Required, StringLength(10), Unicode(false)]
        public string AttedantUserCode { get; set; } = string.Empty;
        [Required, StringLength(50), Unicode(false)]
        public string AssignedBy { get; set; } = string.Empty;
        public DateTime DateAssigned { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;


    }
    public class DispenserAssignmentDto
    {
        [Required, StringLength(5), Unicode(false)]
        public string DispenserCode { get; set; } = string.Empty;
        [Required, StringLength(5), Unicode(false)]
        public string StationCode { get; set; } = string.Empty;
        [Required, StringLength(7), Unicode(false)]
        public string AttedantUserCode { get; set; } = string.Empty;
    }
}
