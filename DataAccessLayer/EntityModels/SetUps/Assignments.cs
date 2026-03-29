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
    public class I : BaseEntity
    {
        [Required, StringLength(20), Unicode(false)]
        public string AssignedCode { get; set; } = string.Empty;
        [Required, StringLength(20), Unicode(false)]
        public string TypeOfAssignment { get; set; } = string.Empty;
        [Required, StringLength(20), Unicode(false)]
        public string UserAssignedTo { get; set;} = string.Empty;
		public bool IsActive { get; set; }
    }
}
