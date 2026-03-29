using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.DTOs.EmailDtos
{
    public class SendEmailDto
    {
        [StringLength(maximumLength: 1000), Unicode(false), Required]
        public string To { get; set; } = string.Empty;
        [StringLength(maximumLength: 100), Unicode(false), Required]
        public string From { get; set; } = string.Empty;
        [StringLength(maximumLength: 100), Unicode(false), Required]
        public string Subject { get; set; } = string.Empty;
        [StringLength(maximumLength: 500), Unicode(false), Required]
        public string Body { get; set; } = string.Empty;
        [StringLength(maximumLength: 40), Unicode(false), Required]
        public string ReportName { get; set; } = string.Empty;
        public TimeOnly SendTime { get; set; }

    }
}
