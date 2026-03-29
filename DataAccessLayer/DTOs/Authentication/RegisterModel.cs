using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.DTOs.Authentication
{
    public class RegisterModel
    {
        [Required,StringLength(50, ErrorMessage = nameof(FirstName))]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        public string MiddName { get; set; } = string.Empty;
        [EmailAddress, CompanyEmail]
        public string Email { get; set; } = string.Empty;
        [Required,Unicode(false),MinLength(10)]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public string PayrollNumber { get; set; } = string.Empty;
        public List<int> AccessApps { get; set; } = new List<int>();

        
    }



    internal class CompanyEmailAttribute : Attribute
    {
        public static string Email(string email)
        {
            if (!email.EndsWith("@protoenergy.com"))
            {
                throw new Exception("Kindly Use CompanyEmail");
            };
            return email;
        }

    }

    public class UpdateUsers
    {
        [Required, StringLength(50, ErrorMessage = nameof(FirstName))]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        public string MiddName { get; set; } = string.Empty;
        [Required, EmailAddress, CompanyEmail]
        public string Email { get; set; } = string.Empty;
        [Required, Unicode(false), MinLength(10)]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public string PayrollNumber { get; set; } = string.Empty;
    }

}
