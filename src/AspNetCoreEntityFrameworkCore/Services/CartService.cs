using AspNetCoreEntityFrameworkCore.DataAccess.Model;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreEntityFrameworkCore.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AspNetCoreEntityFrameworkCore.Services
{
    public interface ICartService
    {
        Task AddItem(int userId, int productId, int quantity);

        Task BuyCart(int userId);

        Task<Cart> GetCart(int userId);

        Task RemoveItem(int userId, int productId);
    }

    public class CartService : ICartService
    {
        private readonly ILogger<CartService> _logger;
        private readonly CommerceDbContext _commerceDbContext;

        public CartService(ILogger<CartService> logger, CommerceDbContext commerceDbContext)
        {
            _logger = logger;
            _commerceDbContext = commerceDbContext;
        }

        public async Task AddItem(int userId, int productId, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity), quantity, "Must be greater than 0.");

            _logger.LogInformation("User {UserId} request to add the item {ProductId}.", userId, productId);

            _logger.LogInformation("Calling database");

            Product product = await _commerceDbContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ProductId == productId);

            if (product == null)
            {
                throw new Exception($"Product id {productId} not found!");
            }

            Func<CartItem> newCartItem = () => new CartItem
            {
                ProductId = product.ProductId,
                Description = product.Description,
                Price = product.Price,
                Quantity = quantity
            };

            Cart cart = await _commerceDbContext.Carts
                .FirstOrDefaultAsync(x => x.UserId == userId && x.PurchaseDate == null);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    Items = new List<CartItem>
                    {
                        newCartItem()
                    }
                };
                _commerceDbContext.Carts.Add(cart);
            }
            else
            {
                CartItem cartItem = await _commerceDbContext.CartItems
                    .FirstOrDefaultAsync(x => x.CartId == cart.CartId && x.ProductId == productId);

                if (cartItem != null)
                {
                    cartItem.Quantity = cartItem.Quantity + quantity;
                }
                else
                {
                    cartItem = newCartItem();
                    cart.Items.Add(cartItem);
                }
            }

            await _commerceDbContext.SaveChangesAsync();

            _logger.LogInformation("Cart item has beed added.");
        }

        public async Task BuyCart(int userId)
        {
            _logger.LogInformation("User {UserId} request to buy its cart.", userId);

            _logger.LogInformation("Calling database");

            Cart cart = await _commerceDbContext.Carts
                .Include(x => x.Items)
                .FirstOrDefaultAsync(x => x.UserId == userId && x.PurchaseDate == null);

            if (cart.Items.Count > 0)
            {
                cart.PurchaseDate = DateTime.Now;
            }

            await _commerceDbContext.SaveChangesAsync();

            _logger.LogInformation("Cart has beed accepted.");
        }

        public async Task<Cart> GetCart(int userId)
        {
            _logger.LogInformation("User {UserId} request to retrieve all the cart info.", userId);

            _logger.LogInformation("Calling database");

            Cart cart = await _commerceDbContext.Carts
                .AsNoTracking()
                .Include(x => x.Items)
                    .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.UserId == userId && x.PurchaseDate == null);

            _logger.LogInformation("Cart item has beed retrieved.");

            return cart;
        }

        public async Task RemoveItem(int userId, int productId)
        {
            _logger.LogInformation("User {UserId} request to remove the item {ProductId}.", userId, productId);

            _logger.LogInformation("Calling database");

            Cart cart = await _commerceDbContext.Carts
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId && x.PurchaseDate == null);

            if (cart != null)
            {
                CartItem cartItem = await _commerceDbContext.CartItems
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.CartId == cart.CartId && x.ProductId == productId);

                _commerceDbContext.CartItems.Remove(cartItem);
            }

            await _commerceDbContext.SaveChangesAsync();

            _logger.LogInformation("Cart item has beed removed.");
        }
    }
}
