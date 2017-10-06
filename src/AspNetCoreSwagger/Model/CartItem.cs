namespace AspNetCoreSwagger.Model
{
    public class CartItem
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
