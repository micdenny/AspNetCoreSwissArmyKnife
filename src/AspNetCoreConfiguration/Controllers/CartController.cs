using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreConfiguration.Services;

namespace AspNetCoreConfiguration.Controllers
{
    [Produces("application/json")]
    [Route("api/cart")]
    public class CartController : Controller
    {
        private int _loggedUserId = 100;

        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<object> Get()
        {
            // return the cart with all its items
            var cart = await _cartService.GetCart(_loggedUserId);
            return cart;
        }

        [HttpPut("{itemId}")]
        public async Task Put(int itemId)
        {
            // add an item to the cart
            await _cartService.AddItem(_loggedUserId, itemId);
        }

        [HttpPost]
        public async Task Post()
        {
            // buy all the items in the cart
            await _cartService.BuyCart(_loggedUserId);
        }

        [HttpDelete("{itemId}")]
        public async Task Delete(int itemId)
        {
            // remove an item from the cart
            await _cartService.RemoveItem(_loggedUserId, itemId);
        }
    }
}