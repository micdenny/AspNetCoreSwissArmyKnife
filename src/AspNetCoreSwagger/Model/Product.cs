using System.ComponentModel.DataAnnotations;

namespace AspNetCoreSwagger.Model
{
    public class Product
    {
        public Product(int id, string description, decimal price)
        {
            this.Id = id;
            this.Description = description;
            this.Price = price;
        }

        [Required]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
