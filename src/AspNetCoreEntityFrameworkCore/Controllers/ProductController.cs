using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;
using AspNetCoreEntityFrameworkCore.Services;
using System.Threading.Tasks;
using AspNetCoreEntityFrameworkCore.Model;
using AspNetCoreEntityFrameworkCore.DataAccess.Model;

namespace AspNetCoreEntityFrameworkCore.Controllers
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
        public async Task<AddProductResponse> Post([FromBody]AddProductRequest request)
        {
            Product product = await _productService.AddProduct(request.Description, request.Price);
            return new AddProductResponse
            {
                ProductId = product.ProductId
            };
        }

        [SwaggerOperation("UpdateProduct")]
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody]UpdateProductRequest product)
        {
            await _productService.UpdateProduct(id, product.Description, product.Price);
        }

        [SwaggerOperation("DeleteProduct")]
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _productService.DeleteProduct(id);
        }
    }
}
