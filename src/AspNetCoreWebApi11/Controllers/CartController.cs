using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreWebApi11.Controllers
{
    [Route("api/[controller]")]
    public class CartController : Controller
    {
        [HttpGet]
        public string Get()
        {
            // return the cart with all its items
            return "This is the cart with all its items, don't you see it?!";
        }

        [HttpPut("{itemId}")]
        public void Put(int itemId)
        {
            // add an item to the cart
        }

        [HttpPost]
        public void Post()
        {
            // buy all the items in the cart
        }

        [HttpDelete("{itemId}")]
        public void Delete(int itemId)
        {
            // remove an item from the cart
        }
    }
}
