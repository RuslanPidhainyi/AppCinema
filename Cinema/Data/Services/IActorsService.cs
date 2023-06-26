using AppCinema.Data.Base;
using AppCinema.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppCinema.Data.Services
{
    public interface IActorsService : IEntityBaseRepository<Actor>
    {
        //IEntityBaseRepository - jest interfacem funckij dla implementacji funkcji wszystkich modelej (dla Actor, Cinema, Movies i td)
        //Zrobiono to z tego celu, zeby nie powtarzać się wilokrotno. Zeby nie pisać dla kozdego kontrolera te same funkcji 

        //To juz nam nie potrzebno tak jak wykorzystamy IEntityBaseRepository.
        /*

        //GetAllAsync
        //Отримання всіх акторів із бази
        Task<IEnumerable<Actor>>GetAllAsync();


        //GetByIdAsync
        //Отримання(Повернення) одного актора
        Task<Actor> GetByIdAsync(int id);


        //AddAsync
        //Метод - додав. даних до БД, і як тільки ми додамо дані до БД,
        //ми не повертатимемо жодних даних користувачеві, тому просто уникайте цього типу повернення в цьому випадку - це (void)  
        Task AddAsync (Actor actor); //Task AddAsync - bo mielismy void, który nic nie zwracał ( void Add(Actor actor); => Task AddAsync (Actor actor); )


        //Update
        //Отримуємо одного актора, і змінюємо дані про нього.
        //Маємо перший параметр ID, ми збираємось провірити його сутність(вмістимість), перед тим як обновимо дані актора і якщо він є - то ми збираємося обновити дані актора.
        //А якщо немає цього актора, тоді нічого не зроби 
        Task<Actor> UpdateAsync(int id, Actor newActor);


        //Delete
        //Знаходить по ID актора і видаляє його
        Task DeleteAsync(int id);

        */
    }
}
