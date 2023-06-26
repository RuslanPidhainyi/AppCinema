using AppCinema.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AppCinema.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        //EntityBaseRepository - методы базавой сущности репозитории.  

        //EntityBaseRepository - jest interfacem funckij dla implementacji funkcji wszystkich modelej (dla Actor, Cinema, Movies i td)
        //Zrobiono to z tego celu, zeby nie powtarzać się wilokrotno. Zeby nie pisać dla kozdego kontrolera te same funkcji 

        private readonly AppDbContext _context;

        //ctor
        public EntityBaseRepository(AppDbContext context)
        {
            _context = context;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //AddAsync
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);

            await _context.SaveChangesAsync(); 
        }

        //GetAllAsync
        public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

 
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //GetAllAsync 
        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            //І тоді ви спочатку зайшли, сказали сутність у порожній контекстний файл.

            //І тоді ми матимемо контекст, який встановлює метод і найкращий параметр.
            IQueryable<T> query = _context.Set<T>();

            //Тепер, коли у нас є запит, ми просто додамо всі властивості до всіх властивостей доходу

            /*
             1 - ми збираємося використовувати агрегатну функцію, оскільки ми хочемо отримати всі властивості з включеного
                 масив властивостей.
                 Для цього просто введіть свій агрегат.

             2 - Першим периметром агрегату буде сам запит.

             3 - А другим параметром буде функція, яка використовуватиметься для отримання всіх властивостей include
                  таким новим, що ми збираємося мати поточний і include власність.

             4 - І потім для кожного поточного значення ми збираємося мати includeProperty(включину властивість)
            */
                
                                        //1      //2   //3                                   //4
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.ToListAsync();

        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //GetByIdAsync
        public async Task<T> GetByIdAsync(int id) =>  await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);


        //UpdateAsync
        public async Task UpdateAsync(int id, T entity)
        {
            //Tutaj zbiramy się wykorzystać (запись сущности класса записи обьекта)


            //imprtowanie przstrzen nazw dla zapisu objekcta, ktore jest dla sledzenia zmian kropki (ядра структуры обьекта)  
            EntityEntry entityEntry = _context.Entry<T>(entity);


            //Odswiezyć zapis wykorzystania struktyry (сущности).
            //Potrzebno ustawić stan objecta tego zapisu
            entityEntry.State = EntityState.Modified;

            await _context.SaveChangesAsync();

        }


        //DeleteAsync
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;

            await _context.SaveChangesAsync();

        }
    }
}
