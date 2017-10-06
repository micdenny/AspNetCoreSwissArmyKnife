using System.ComponentModel.DataAnnotations;

namespace AspNetCoreEntityFrameworkCore.Model
{
    public class UpdateProductRequest
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
