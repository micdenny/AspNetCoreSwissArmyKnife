using System.Threading.Tasks;
using AspNetCoreEntityFrameworkCore.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AspNetCoreEntityFrameworkCore.Controllers
{
    [Route("api/[controller]")]
    public class CartItemController : Controller
    {
        private int _loggedUserId = 100;

        private readonly ICartService _cartService;

        public CartItemController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [SwaggerOperation("AddItemToCart")]
        [HttpPut("{productId}/quantity/{quantity}")]
        public async Task Put(int productId, int quantity)
        {
            // add an item to the cart
            await _cartService.AddItem(_loggedUserId, productId, quantity);
        }

        [SwaggerOperation("RemoveItemFromCart")]
        [HttpDelete("{productId}")]
        public async Task Delete(int productId)
        {
            // remove an item from the cart
            await _cartService.RemoveItem(_loggedUserId, productId);
        }
    }
}
