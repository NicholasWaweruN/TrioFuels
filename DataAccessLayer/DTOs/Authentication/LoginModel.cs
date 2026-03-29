using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;

namespace DataAccessLayer.DTOs.Authentication
{
    public class EmailLoginModel
    {
        [Required, StringLength(50, ErrorMessage = "UserName Can Not Be More Then 50 Characters")]
        public string UserName { get; set; } = string.Empty;
        [Required, StringLength(50, ErrorMessage = "Password Can Not Be More Then 50 Characters")]
        public string Password { get; set; } = string.Empty;
        public string AppCode { get; set; } = string.Empty;
        public string PdaDeviceImei { get; set; } = string.Empty;
		public string VersionCode {get; set; } = string.Empty;
    }
    //
    public class PhoneLoginModel
    {
        [Required, StringLength(50, ErrorMessage = "PhoneNumber Can Not Be More Then 50 Characters")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required, StringLength(50, ErrorMessage = "Password Can Not Be More Then 50 Characters")]
        public string Password { get; set; } = string.Empty;
    }
    public class UserDetails
    {
        public string? UserCode { get; set; } = string.Empty;
        public string? Token { get; set; } = string.Empty;
        public List<RolesDto> Roles { get; set; } = new List<RolesDto>();
        public List<AccessTypes> AccessApps{ get; set; } = new List<AccessTypes>();

    }
    public class UserDetailsDispensers
    {
		public bool IsTochangePassword { get; set; } = false;
		public string? UserCode { get; set; } = string.Empty;
        public string? Token { get; set; } = string.Empty;
		public string? Name {  get; set; } = string.Empty;
        public List<RolesDto> Roles { get; set; } = new List<RolesDto>();
        public List<AccessTypes> AccessApps { get; set; } = new List<AccessTypes>();
        public NozzleDetails NozzleDetails { get; set; } = new NozzleDetails();

    }
public class DispenserNozzles
{
    public string DispenserName { get; set; } = string.Empty;
    public string StationName { get; set; } = string.Empty;
    public string TillNumber { get; set; } = string.Empty;
    public List<Nozzles> Nozzles { get; set; } = new List<Nozzles>();
}
public class Nozzles
{
    public string NozzleCode { get; set; } = string.Empty;
    public string NozzleName { get; set; } = string.Empty;
}
	public class NozzleDetails
	{
		public string DispenserCode { get; set; } = string.Empty;
		public string DispenserName { get; set; } = string.Empty;
		public string StationName { get; set; } = string.Empty;
		public string StationCode { get; set; } = string.Empty;
		public string TillNumber { get; set; } = string.Empty;
		public string StoreNumber { get; set; } = string.Empty;
		public List<Nozzles> Nozzles { get; set; } = new List<Nozzles>();
	}
public class RolesDto
    {
        public string RoleName { get; set; } = string.Empty;
    }
    public class AccessTypes
    {
        public string AccessApp { get; set; } = string.Empty;
        public string AppsCode { get; set; } = string.Empty;

    }

    public class Nozzleslist
    {
        public string? NozzleCode { get; set; } = string.Empty;
        public string? NozzleName { get; set; } = string.Empty;
    }

    public class ImeiList
    {
        public string ImeiNumber { get; set; } = string.Empty;
    }
    public class ApplicationUserDto 
    {
        public string Names { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public string PayrollNumber { get; set; } = string.Empty;
        public string UserCode { get; set; } = string.Empty;
        public  string PhoneNumber { get; set; } = string.Empty;
        public  string UserName { get; set; } = string.Empty;
        public  string Email { get; set; } = string.Empty;
        public DateTime  DateAdded { get; set; }
    }
    public class UserDetailsDto
    {
        public string MiddName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public string PayrollNumber { get; set; } = string.Empty;
        public string UserCode { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime DateAdded { get; set; }

    }
    public class TokenDto
    {
        public List<RolesDto> Roles { get; set; } = new List<RolesDto>();
        public string Tooken { get; set; } = string.Empty;

    }

    public class TokenDetails
    {
        public string Tooken { get; set; } = string.Empty;     // The JWT or authentication token
        public DateTime Expiration { get; set; }   // Expiration date and time of the token
        public List<RolesDto> Roles { get; set; } = new List<RolesDto>();   // List of roles assigned to the user
        public string RefreshToken { get; set; } = string.Empty;    // Optional refresh token for renewing the access token
        public DateTime RefreshTokenExpiration { get; set; } // Expiration of the refresh token
        public string UserCode { get; set; } = string.Empty;          // The unique identifier of the user
    }
}
