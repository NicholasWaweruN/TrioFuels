
using DataAccessLayer.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Authentication.Entity
{
	public class ApplicationUser : IdentityUser
	{
		public override string Id { get; set; } = Guid.NewGuid().ToString();
		[Required, StringLength(30), Unicode(false)]
		public string FirstName { get; set; } = string.Empty;
		[Required, StringLength(30), Unicode(false)]
		public string LastName { get; set; } = string.Empty;
		[Required, StringLength(30), Unicode(false)]
		public string MiddName { get; set; } = string.Empty;
		public bool IsActive { get; set; } = true;
		[Required, StringLength(20), Unicode(false)]
		public string PayrollNumber { get; set; } = string.Empty;
		[Required, StringLength(10), Unicode(false)]
		public string UserCode { get; set; } = string.Empty;
		public DateTime DateCreated { get; set; } = DateTime.UtcNow;
		public DateTime DateModified { get; set; } = DateTime.UtcNow;
		[StringLength(50), Unicode(false)]
		public string CreatedBy { get; set; } = string.Empty;
		[StringLength(50), Unicode(false)]
		public string ModifiedBy { get; set; } = string.Empty;
		public DateTime? PasswordLastUpdated { get; set; }
		[Required, StringLength(4), Unicode(false), EmailAddress]
		public string AccessApps { get; set; } = string.Empty;
		[StringLength(15), Unicode(false)]
		public override string? PhoneNumber { get; set; } = string.Empty;
		[Required, StringLength(30), Unicode(false)]
		public override string? UserName { get; set; } = string.Empty;
		[Required, StringLength(40), Unicode(false), EmailAddress]
		public override string? Email { get; set; } = string.Empty;
		[Required, StringLength(50), Unicode(false)]
		public override string? NormalizedEmail { get; set; } = string.Empty;
		[Required, StringLength(50), Unicode(false)]
		public string StationCode { get; set; } = string.Empty;
		public int UserType { get; set; } = 1;
		[Required, StringLength(50), Unicode(false)]
		public string DepartmentCode { get; set; } = string.Empty;
		public DateTime? LastLoginDate { get; set; }
	}

#nullable restore
	
    public class UserRoles : IdentityRole

	{
		[Required, StringLength(150), Unicode(false)]
		public string ApiPermission { get; set; } = string.Empty;
	}

	public class PasswordHistory : BaseEntity
	{
		[Required, StringLength(100), Unicode(false)]
		public string PasswordHash { get; set; } = string.Empty;
	}
	

	public class UserTypes
	{
		[Required, StringLength(50), Unicode(false)]
		public string UserType { get; set; } = string.Empty;
		[Required, StringLength(50), Unicode(false)]
		[Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int UserTypeId  { get; set; } 
	}


	public class ApiPermisions : BaseEntity
    {
        [Required, StringLength(140), Unicode(false)]
        public string RoleId { get; set; } = string.Empty;
        [Required, StringLength(150), Unicode(false)]
        public string ApiPermission { get; set; }= string.Empty;
    }



    public class Role : BaseEntity
    {
        [Required, StringLength(200), Unicode(false)]
        public string RoleName { get; set; } = string.Empty;
        [Required, StringLength(200), Unicode(false)]
        public string RoleCode { get; set; } = string.Empty;

    }
    public class RoleAndPermisions : BaseEntity
    {
        [Required, StringLength(200), Unicode(false)]
        public string RoleCode { get; set; } = string.Empty;
        [Required, StringLength(200), Unicode(false)]
        public string PermissionCode { get; set; } = string.Empty;
    }

	public class RoleToUser
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required, StringLength(200), Unicode(false)]
		public string RoleCode { get; set; } = string.Empty;
		[Required, StringLength(100), Unicode(false)]
		public string UserCode { get; set; } = string.Empty;
		public DateTime DateCreated {  get; set; }
	}
	


    public class ProtoApps 
    {
        [Required,StringLength(20),Unicode(false)]
        public string AppsName { get; set; } = string.Empty;
        [Required,StringLength(10),Unicode(false)]
        public string AppsCode { get; set; } = string.Empty;
        [Required,StringLength(10)]
        public string UserCode { get; set; } = string.Empty;
        [Required,StringLength(50),Unicode(false)]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        [Required,StringLength(20),Unicode(false)]
        public string  CurrentVersion { get; set; } = string.Empty;

    }
    public class UserApps 
    {
        [Required,StringLength(10),Unicode(false)]
        public string AppsCode { get; set; } = string.Empty;
        [Required,StringLength(10),Unicode(false)]
        public string UserCode { get; set; } = string.Empty;
        [Required, StringLength(50), Unicode(false)]
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
