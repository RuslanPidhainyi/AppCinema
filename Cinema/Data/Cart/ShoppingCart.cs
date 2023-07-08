using AppCinema.Models;
using Microsoft.AspNetCore.Http; // for ISession
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection; //for GetRequiredService
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppCinema.Data.Cart
{
    public class ShoppingCart
    {

        public AppDbContext _context { get; set; }

        public string ShoppingCartId { get; set; }  

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }

        //цей метод кошик для покупок або просто картку отримання.
        /*
         Для цього я просто введу public, і цей метод буде статичним, тому що я збираюся використовувати цей метод у цій базі даних для файлу.

         Тип повернення буде типом повернення кошика для покупок, а ім’я – отримати кошик для покупок.
         
         потім це візьме нам електролічильник, постачальник послуг. Тож я постачальник послуг, а протягом року просто обслуговування.

         тут ми матимемо послуги, і нам потрібні ці послуги, щоб отримати цей сеанс і перевірити якщо ми вже маємо сеанс із цією карткою

         В іншому випадку ми створимо новий ідентифікатор і передамо цей ідентифікатор двом новим сеансам.
         */
        public static ShoppingCart GetShoppingCart (IServiceProvider services)
        {
            /*
             Отже, для цього я введу сесію II, а потім імпортую в простір імен. І тоді самоствердження I сеансу дорівнює тому, що буде дорівнювати необхідним послугам обслуговування.
             Тож отримайте необхідну послугу. 
             Тоді я підтримую простір імен, і простір імен буде Microsoft, що розширює цю залежність ін'єкція.
             А потім ми збираємося вставити наступника контексту IHttp.
             А потім тут, якщо це не зараз, тоді IHttp контекст цього сеансу
             */
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            //Отже, таким чином ми можемо отримати цей сеанс за допомогою постачальника послуг.

            /*
              отримати послугу, і ми потрапимо сюди, контекст БД.
             */
            var context = services.GetService<AppDbContext>();

            //А потім ми перевіримо, чи є у нас сеанс ідентифікації кошика

            /*
             Отже, для цього рядка ідентифікатор кошика дорівнює сильному сеансу, 
             і ми будемо шукати правильний ідентифікатор В іншому випадку, якщо це зараз, ми збираємося створити новий ідентифікатор кошика. 
             Тож Guid постав, новий Guid, а потім це, щоб ввести це, вони збираються встановити сеанс
             */
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();


            /*
             сеанс, встановити рядок.А тут у нас є ідентифікатор кошика, встановлений на ідентифікатор кошика.
             */
            session.SetString("CartId", cartId);


            return new ShoppingCart(context)
            {
                ShoppingCartId = cartId
            };
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void AddItemToCart(Movie movie)
        {
            var shoppingCartItem = _context.ShoppingCartItems
                .FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Movie = movie, 
                    Amout = 1
                };
                _context.ShoppingCartItems.Add(shoppingCartItem); 
            }
            else
            {
                shoppingCartItem.Amout++;
            }
            _context.SaveChanges();
        }


        public void RemoveItemFromCart(Movie movie)
        {
            var shoppingCartItem = _context.ShoppingCartItems
                .FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amout > 1)
                {
                    shoppingCartItem.Amout--;
                }
                else
                {
                    _context.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            _context.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartsItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItems
                .Where(n => n.ShoppingCartId == ShoppingCartId)
                .Include(n => n.Movie).ToList()); 
        }

        public double GetShoppingCartTotal()
        {
            var total = _context.ShoppingCartItems
                .Where(n => n.ShoppingCartId == ShoppingCartId)
                .Select(n => n.Movie.Price * n.Amout)
                .Sum();
            
            return total;
        }

        public async Task ClearShoppingCartAsync()
        {
            var items = await _context.ShoppingCartItems.Where(n => n.ShoppingCartId == ShoppingCartId).ToListAsync();

            _context.ShoppingCartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
