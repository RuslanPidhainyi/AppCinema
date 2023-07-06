using AppCinema.Data.Cart;
using AppCinema.Data.Services;
using AppCinema.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AppCinema.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly ShoppingCart _shoppingCart;

        public OrdersController(IMoviesService moviesService, ShoppingCart shoppingCart)
        {
            _moviesService = moviesService;
            _shoppingCart = shoppingCart;
        }

        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartsItems();
            
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(response);
        }

        //RedirectToActionResult - результатом перенаправлення до дії
        //public async Task<RedirectToActionResult> AddItemToShoppingCart(int Id)
        public async Task<IActionResult> AddItemToShoppingCart(int Id)
        {
            var item = await _moviesService.GetMovieByIdAsync(Id);

            if(item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemoveItemToShoppingCart(int Id)
        {
            var item = await _moviesService.GetMovieByIdAsync(Id);

            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

    }
}
