using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreWebApi20.Services;

namespace AspNetCoreWebApi20.Controllers
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

        [HttpGet]
        public async Task<object> Get()
        {
            // return the cart with all its items
            var cart = await _cartService.GetCart(_loggedUserId);
            return cart;
        }

        [HttpPut("{itemId}")]
        public void Put(int itemId)
        {
            // add an item to the cart
            _cartService.AddItem(_loggedUserId, itemId);
        }

        [HttpPost]
        public void Post()
        {
            // buy all the items in the cart
            _cartService.BuyCart(_loggedUserId);
        }

        [HttpDelete("{itemId}")]
        public void Delete(int itemId)
        {
            // remove an item from the cart
            _cartService.RemoveItem(_loggedUserId, itemId);
        }
    }
}
