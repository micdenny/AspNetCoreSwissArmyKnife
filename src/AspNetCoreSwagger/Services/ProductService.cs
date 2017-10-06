using AspNetCoreSwagger.Model;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreSwagger.Services
{
    public interface IProductService
    {
        Task<Product> AddProduct(Product product);
        Task DeleteProduct(int id);
        Task<Product> GetProduct(int id);
        Task<List<Product>> GetProducts();
        Task UpdateProduct(Product product);
    }

    public class ProductService : IProductService
    {
        private static Dictionary<int, Product> Storage = new Dictionary<int, Product>
        {
            { 1, new Product(1, "value1", 10m) },
            { 2, new Product(2, "value2", 22.9m) }
        };

        private readonly ProductServiceOptions _options;

        public ProductService(IOptions<ProductServiceOptions> options)
        {
            _options = options.Value;
        }

        public async Task<Product> AddProduct(Product product)
        {
            await Task.Delay(100);
            int id = 1;
            if (Storage.Count > 0)
            {
                id = Storage.Keys.Max() + 1;
            }
            product.Id = id;
            Storage.Add(id, product);
            return product;
        }

        public async Task DeleteProduct(int id)
        {
            await Task.Delay(100);
            Storage.Remove(id);
        }

        public async Task<Product> GetProduct(int id)
        {
            await Task.Delay(100);
            if (Storage.ContainsKey(id))
            {
                return Storage[id];
            }
            return null;
        }

        public async Task<List<Product>> GetProducts()
        {
            await Task.Delay(100);
            return Storage.Values.OrderBy(x => x.Id).ToList();
        }

        public async Task UpdateProduct(Product product)
        {
            await Task.Delay(100);
            Storage[product.Id] = new Product(product.Id, product.Description, product.Price);
        }
    }
}
