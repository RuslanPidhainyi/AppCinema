﻿using AppCinema.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppCinema.Data.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly AppDbContext _context;

        public OrdersService(AppDbContext context)
        {
            _context = context;
        }


        //бажанням отримати всі замовлення з бази даних.
        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
        {
            /*
              Отже, ми зараз отримуємо всі замовлення, відфільтровані за ідентифікатором користувача, тому давайте просто видалимо
              ідентифікатор користувача ".Where(n => n.UserId == userId)",

              оскільки якщо користувач, який увійшов у наш додаток, є адміністратором, адміністратори зможуть
              щоб переглянути всі замовлення в додатку.  
            */
            var orders = await _context.Orders.Include(n => n.OrderItems).ThenInclude(n => n.Movie).Include(n => n.User).ToListAsync(); 
            
            if(userRole != "Admin")
            {
                orders = orders.Where(n => n.UserId == userId).ToList();
            }
            return orders;
        }

        //додавання замовлень до бази даних,
        public async Task StoreOredrAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {

            /*
             Кожне замовлення матиме унікальний ідентифікатор ID. замовлення та ID у замовленні має
             декоратор ключа, що означає, що ідентифікатор буде згенеровано з бази даних.
                
            І вам просто потрібно буде надати ідентифікатор користувача., ідентифікатор користувача.

            І після ідентифікатора користувача ми матимемо електронну адресу користувача
             */
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress,
            };

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            //Ми збираємося зберегти всі елементи кошика для покупок у базі даних. Тому для цього я збираюся використовувати для кожного циклу.

            foreach (var e in items) 
            {
                var orderItem = new OrderItem()
                {
                    Amout = e.Amout,
                    MovieId = e.Movie.Id,
                    OrderId = order.Id,
                    Price = e.Movie.Price
                };

               await _context.OrderItems.AddAsync(orderItem);
            }

            await _context.SaveChangesAsync();
        }
    }
}
