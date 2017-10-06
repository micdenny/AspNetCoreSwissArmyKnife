using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreEntityFrameworkCore.DataAccess.Model
{
    public class Cart
    {
        [Required]
        public int CartId { get; set; }

        [Required]
        public int UserId { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
