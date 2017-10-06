using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using AspNetCoreSwagger.Model;
using AspNetCoreSwagger.Services;

namespace AspNetCoreSwagger.Controllers
{
    [Route("api/[controller]")]
    public class CartController : Controller
    {
        private int _loggedUserId = 100;

        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [SwaggerOperation("GetCart")]
        [HttpGet]
        public async Task<Cart> Get()
        {
            // return the cart with all its items
            var cart = await _cartService.GetCart(_loggedUserId);
            return cart;
        }

        [SwaggerOperation("AddItemToCart")]
        [HttpPut("{productId}")]
        public async Task Put(int productId)
        {
            // add an item to the cart
            await _cartService.AddItem(_loggedUserId, productId);
        }

        [SwaggerOperation("BuyCart")]
        [HttpPost]
        public async Task Post()
        {
            // buy all the items in the cart
            await _cartService.BuyCart(_loggedUserId);
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
