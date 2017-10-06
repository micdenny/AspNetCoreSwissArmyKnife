using AspNetCoreEntityFrameworkCore.DataAccess.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreEntityFrameworkCore.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreEntityFrameworkCore.Services
{
    public interface IProductService
    {
        Task<Product> AddProduct(string description, decimal price);
        Task DeleteProduct(int id);
        Task<Product> GetProduct(int id);
        Task<List<Product>> GetProducts();
        Task UpdateProduct(int id, string description, decimal price);
    }

    public class ProductService : IProductService
    {
        private readonly CommerceDbContext _commerceDbContext;

        public ProductService(CommerceDbContext commerceDbContext)
        {
            _commerceDbContext = commerceDbContext;
        }

        public async Task<Product> AddProduct(string description, decimal price)
        {
            var product = new Product
            {
                Description = description,
                Price = price
            };
            _commerceDbContext.Products.Add(product);
            await _commerceDbContext.SaveChangesAsync();
            return product;
        }

        public async Task DeleteProduct(int id)
        {
            _commerceDbContext.Products.Remove(new Product
            {
                ProductId = id
            });
            await _commerceDbContext.SaveChangesAsync();
        }

        public async Task<Product> GetProduct(int id)
        {
            var product = await _commerceDbContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ProductId == id);

            return product;
        }

        public async Task<List<Product>> GetProducts()
        {
            var products = await _commerceDbContext.Products
                .ToListAsync();

            return products;
        }

        public async Task UpdateProduct(int id, string description, decimal price)
        {
            _commerceDbContext.Products.Update(new Product
            {
                ProductId = id,
                Description = description,
                Price = price
            });
            await _commerceDbContext.SaveChangesAsync();
        }
    }
}
