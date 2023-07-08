using AppCinema.Data.ViewComponents;
using AppCinema.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppCinema.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOredrAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress);

        Task<List<Order>> GetOrdersByUserIdAsync(string userId);
    }
}
