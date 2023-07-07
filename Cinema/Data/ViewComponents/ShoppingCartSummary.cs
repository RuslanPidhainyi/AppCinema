using AppCinema.Data.Cart;
using Microsoft.AspNetCore.Mvc; // for ViewComponent

namespace AppCinema.Data.ViewComponents
{
    //[ViewComponent] Variant1 - you can use ViewComponent like this  
    public class ShoppingCartSummary : ViewComponent // Variant 2 - you can use ViewComponent like this
    {
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public IViewComponentResult Invoke() //invoke - викликати
        {
            //Отже, у нас є список елементів кошика для покупок, або ви можете просто отримати кількість кошика для покупок
            var items = _shoppingCart.GetShoppingCartsItems();

            //У цьому випадку я збираюся лише повернути кількість елементів.
            //Просто поверніться.
            //Нам потрібно повернутися до її поля зору, а потім до її предметів, які мають значення.
            return View(items.Count);
        }
    }
}
