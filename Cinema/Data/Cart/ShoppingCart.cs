using AppCinema.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AppCinema.Data.Cart
{
    public class ShoppingCart
    {
        public ShoppingCart(AppDbContext context)
        {
            _context = context;
        }

        public AppDbContext _context { get; set; }

        public string ShoppingCartId { get; set; }  

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }


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
    }
}
