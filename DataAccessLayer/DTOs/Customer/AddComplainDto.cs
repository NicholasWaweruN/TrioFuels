using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.CustomerService
{
    public class AddComplainDto
    {
        [Required]
        public string VehicleCode { get; set; } = string.Empty;
        [Required]
        public string CustomerCode { get; set; } = string.Empty;
        [Required]
        public string ComplainDescription { get; set; } = string.Empty;
    }
}