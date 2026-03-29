using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.SetupService
{
    public class AddProductDto
    {
        [Required,MaxLength(50)]
        public string ProductName { get; set; } = string.Empty;
    }
}