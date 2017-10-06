using System.ComponentModel.DataAnnotations;

namespace AspNetCoreEntityFrameworkCore.DataAccess.Model
{
    public class Product
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
