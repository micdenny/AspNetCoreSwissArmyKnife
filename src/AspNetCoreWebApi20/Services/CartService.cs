using System.Threading.Tasks;

namespace AspNetCoreWebApi20.Services
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
        public async Task AddItem(int userId, int itemId)
        {
            await Task.Delay(1000);
        }

        public async Task BuyCart(int userId)
        {
            await Task.Delay(1000);
        }

        public async Task<object> GetCart(int userId)
        {
            await Task.Delay(1000);
            return "This is your cart, cool isn't it?!";
        }

        public async Task RemoveItem(int userId, int itemId)
        {
            await Task.Delay(1000);
        }
    }
}
