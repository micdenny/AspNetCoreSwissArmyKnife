using System.ComponentModel.DataAnnotations;

namespace AspNetCoreEntityFrameworkCore.DataAccess.Model
{
    public class CartItem
    {
        [Required]
        public int CartId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        public Product Product { get; set; }
    }
}
