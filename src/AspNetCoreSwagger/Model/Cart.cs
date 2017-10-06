using System.Collections.Generic;

namespace AspNetCoreSwagger.Model
{
    public class Cart
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public List<CartItem> Items { get; set; }
    }
}
