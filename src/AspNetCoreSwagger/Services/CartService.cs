using AspNetCoreSwagger.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreSwagger.Services
{
    public interface ICartService
    {
        Task AddItem(int userId, int itemId);

        Task BuyCart(int userId);

        Task<Cart> GetCart(int userId);

        Task RemoveItem(int userId, int itemId);
    }

    public class CartService : ICartService
    {
        private readonly CartServiceOptions _options;
        private readonly ILogger<CartService> _logger;

        public CartService(IOptions<CartServiceOptions> options, ILogger<CartService> logger)
        //public CartService(IOptionsSnapshot<CartServiceOptions> options, ILogger<CartService> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        public async Task AddItem(int userId, int itemId)
        {
            _logger.LogInformation("User {UserId} request to add the item {ItemId}.", userId, itemId);

            var url = _options.ServiceUrl;
            var timeout = _options.DefaultTimeout;

            _logger.LogInformation("Calling web api {Url} with timeout {Timeout}", url, timeout);

            await Task.Delay(100);

            _logger.LogInformation("Cart item has beed added.");
        }

        public async Task BuyCart(int userId)
        {
            _logger.LogInformation("User {UserId} request to buy its cart.", userId);

            var url = _options.ServiceUrl;
            var timeout = _options.BuyTimeout;

            _logger.LogInformation("Calling web api {Url} with timeout {Timeout}", url, timeout);

            await Task.Delay(100);

            _logger.LogInformation("Cart has beed accepted.");
        }

        public async Task<Cart> GetCart(int userId)
        {
            _logger.LogInformation("User {UserId} request to retrieve all the cart info.", userId);

            var url = _options.ServiceUrl;
            var timeout = _options.DefaultTimeout;

            _logger.LogInformation("Calling web api {Url} with timeout {Timeout}", url, timeout);

            await Task.Delay(100);

            _logger.LogInformation("Cart item has beed retrieved.");

            return new Cart
            {
                CartId = 1,
                UserId = userId,
                Items = new List<CartItem>
                {
                    new CartItem
                    {
                        CartId = 1,
                        ProductId = 1,
                        Description = "il TV",
                        Price = 1234.56m,
                        Quantity = 1
                    },
                    new CartItem
                    {
                        CartId = 1,
                        ProductId = 2,
                        Description = "Dash Button",
                        Price = 4.99m,
                        Quantity = 5
                    }
                }
            };
        }

        public async Task RemoveItem(int userId, int itemId)
        {
            _logger.LogInformation("User {UserId} request to remove the item {ItemId}.", userId, itemId);

            var url = _options.ServiceUrl;
            var timeout = _options.DefaultTimeout;

            _logger.LogInformation("Calling web api {Url} with timeout {Timeout}", url, timeout);

            await Task.Delay(100);

            _logger.LogInformation("Cart item has beed removed.");
        }
    }
}
