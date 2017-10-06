using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using AspNetCoreSwagger.Model;
using AspNetCoreSwagger.Services;
using System.Threading.Tasks;

namespace AspNetCoreSwagger.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [SwaggerOperation("GetProducts")]
        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            var products = await _productService.GetProducts();
            return products;
        }

        [SwaggerOperation("GetProduct")]
        [HttpGet("{id}")]
        public async Task<Product> Get(int id)
        {
            var product = await _productService.GetProduct(id);
            return product;
        }

        [SwaggerOperation("AddProduct")]
        [HttpPost]
        public async Task<Product> Post([FromBody]Product product)
        {
            var inserted = await _productService.AddProduct(product);
            return inserted;
        }

        [SwaggerOperation("UpdateProduct")]
        [HttpPut]
        public async Task Put([FromBody]Product product)
        {
            await _productService.UpdateProduct(product);
        }

        [SwaggerOperation("DeleteProduct")]
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _productService.DeleteProduct(id);
        }
    }
}
