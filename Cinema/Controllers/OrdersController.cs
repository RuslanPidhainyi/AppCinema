using AppCinema.Data.Cart;
using AppCinema.Data.Services;
using AppCinema.Data.Static;
using AppCinema.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppCinema.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;

        public async Task<IActionResult> Index()
        {
            /*
             І в цьому випадку я хочу отримати ідентифікатор користувача, який увійшов у систему, тож для цього я просто введу типи ClaimTypes.
            
             ClaimTypes - Вимоги зазвичай представляються у формі пари ключ-значення і можуть містити різноманітну інформацію про користувача,
             таку як ідентифікатор, роль, електронну пошту, тощо.
             
            ClaimTypes містить набір стандартних типів вимог, таких як:

                1. Name: Ім'я користувача.
                2. Role: Роль користувача.
                3. Email: Електронна пошта користувача.
                4. Sid: Керований ідентифікатор користувача.
                5. ClaimType: Загальний тип вимоги.

             */
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var orders = await _ordersService.GetOrdersByUserIdAndRoleAsync(userId, userRole);
            return View(orders); 
        }

        public OrdersController(IMoviesService moviesService, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _moviesService = moviesService;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;

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

        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartsItems();

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            await _ordersService.StoreOredrAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();
            
            return View("OrderCompleted");
        }

    }
}
