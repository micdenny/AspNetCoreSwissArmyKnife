// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace AspNetCoreSwagger.Sdk.Models
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public partial class Cart
    {
        /// <summary>
        /// Initializes a new instance of the Cart class.
        /// </summary>
        public Cart()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the Cart class.
        /// </summary>
        public Cart(int? cartId = default(int?), int? userId = default(int?), IList<CartItem> items = default(IList<CartItem>))
        {
            CartId = cartId;
            UserId = userId;
            Items = items;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "cartId")]
        public int? CartId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "userId")]
        public int? UserId { get; set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "items")]
        public IList<CartItem> Items { get; set; }

    }
}
