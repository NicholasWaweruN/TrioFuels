using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.DTOs.Authentication
{
    public class UpdateUserDto
    {
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;
        [StringLength(30)]
        public string PhoneNumber { get; set; } = string.Empty;
        [StringLength(30)]
        public string PayrollNumber { get; set; } = string.Empty;
    }
	public class ErrorTrailDto
	{
		[Required, StringLength(20), Unicode(false)]
		public string ErrorCode { get; set; } = string.Empty;
		[Required, StringLength(1000), Unicode(false)]
		public string ErrorMessage { get; set; } = string.Empty;
		[Required, StringLength(50), Unicode(false)]
		public string Method { get; set; } = string.Empty;
		public DateTime DateCreated { get; set; } = DateTime.UtcNow;
	}
}