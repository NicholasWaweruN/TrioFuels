using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityModels.SetUps
{
    public class UserTrail 
    {
        public int Id { get; set; }
        [Required, StringLength(20), Unicode(false)]
        public string UserCode { get; set; } = string.Empty;
        [Required, StringLength(100), Unicode(false)]
        public string UserName { get; set; } = string.Empty;
        [Required, StringLength(1000), Unicode(false)]
        public string Message { get; set; } = string.Empty;
		[Required, StringLength(100), Unicode(false)]
		public string ActionType { get; set; } = string.Empty;
		[Required, StringLength(30), Unicode(false)]
		public string ShiftNumber { get; set; } = string.Empty;
		public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }

	public class ErrorTrail
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required, StringLength(20), Unicode(false)]
		public string ErrorCode { get; set; } = string.Empty;
		[Required, StringLength(1000), Unicode(false)]
		public string ErrorMessage{ get; set; } = string.Empty;
		[Required, StringLength(1000), Unicode(false)]
		public string Method { get; set; } = string.Empty;
		[Required, StringLength(1000), Unicode(false)]
		public string StackTrace { get; set; } = string.Empty;
		[Required, StringLength(1000), Unicode(false)]
		public string InnerErrorMessage { get; set; } = string.Empty;
		public DateTime DateCreated { get; set; } = DateTime.UtcNow;
	}
}
