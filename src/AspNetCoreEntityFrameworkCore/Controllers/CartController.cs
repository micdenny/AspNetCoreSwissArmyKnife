using System.Threading.Tasks;
using AspNetCoreEntityFrameworkCore.Services;
using AspNetCoreEntityFrameworkCore.DataAccess.Model;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AspNetCoreEntityFrameworkCore.Controllers
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

        [SwaggerOperation("BuyCart")]
        [HttpPost]
        public async Task Post()
        {
            // buy all the items in the cart
            await _cartService.BuyCart(_loggedUserId);
        }
    }
}
