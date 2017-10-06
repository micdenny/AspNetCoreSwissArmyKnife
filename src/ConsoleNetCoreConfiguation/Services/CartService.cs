using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace ConsoleNetCoreConfiguation.Services
{
    public interface ICartService
    {
        Task AddItem(int userId, int itemId);

        Task BuyCart(int userId);

        Task<object> GetCart(int userId);

        Task RemoveItem(int userId, int itemId);
    }

    public class CartService : ICartService
    {
        private readonly CartServiceOptions _options;
        private readonly ILogger<CartService> _logger;

        public CartService(IOptions<CartServiceOptions> options, ILogger<CartService> logger)
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

            await Task.Delay(1000);

            _logger.LogInformation("Cart item has beed added.");
        }

        public async Task BuyCart(int userId)
        {
            _logger.LogInformation("User {UserId} request to buy its cart.", userId);

            var url = _options.ServiceUrl;
            var timeout = _options.BuyTimeout;

            _logger.LogInformation("Calling web api {Url} with timeout {Timeout}", url, timeout);

            await Task.Delay(1000);

            _logger.LogInformation("Cart has beed accepted.");
        }

        public async Task<object> GetCart(int userId)
        {
            _logger.LogInformation("User {UserId} request to retrieve all the cart info.", userId);

            var url = _options.ServiceUrl;
            var timeout = _options.DefaultTimeout;

            _logger.LogInformation("Calling web api {Url} with timeout {Timeout}", url, timeout);

            await Task.Delay(1000);

            _logger.LogInformation("Cart item has beed retrieved.");

            return "This is your cart, cool isn't it?!";
        }

        public async Task RemoveItem(int userId, int itemId)
        {
            _logger.LogInformation("User {UserId} request to remove the item {ItemId}.", userId, itemId);

            var url = _options.ServiceUrl;
            var timeout = _options.DefaultTimeout;

            _logger.LogInformation("Calling web api {Url} with timeout {Timeout}", url, timeout);

            await Task.Delay(1000);

            _logger.LogInformation("Cart item has beed removed.");
        }
    }
}
