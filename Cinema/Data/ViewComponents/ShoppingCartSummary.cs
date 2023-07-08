using AppCinema.Data.Cart;
using Microsoft.AspNetCore.Mvc; // for ViewComponent

namespace AppCinema.Data.ViewComponents

//з точки зору прискорення, ViewComponent(компоненти перегляду) мережі MVC подібні до partial views(часткових переглядів), але вони набагато потужніші.

/*
 ViewComponent (Компоненти перегляду) - Компоненти перегляду включають той самий поділ проблем і виплат по інвалідності, що знаходяться між ними
контролер і перегляд. 

Ви можете розглядати компонент перегляду як міні-контролер, який відповідає лише за рендеринг фрагмента відповіді, а не повної відповіді

Отже, компоненти перегляду є лише вдосконаленням часткових переглядів, а інша відмінність полягає в тому, що коли
якщо ви використовуєте часткові представлення, ви все ще залежите від контролера, тоді як у компонентах перегляду ви
не потрібен контролер.



Ми збираємося створити компонент перегляду, і ми будемо використовувати цей компонент перегляду для відображення покупок
значок кошика вгорі.

Отже, на панелі навігації, коли ми маємо деякі товари в кошику для покупок.

*/
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
