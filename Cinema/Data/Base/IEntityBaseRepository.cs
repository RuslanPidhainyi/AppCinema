using AppCinema.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AppCinema.Data.Base
{
    //publiczny interface przejmuje parametr T, i oprediliaem typ T jak Klass i dzidziczy IEntityBase
    public interface IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        //IEntityBaseRepository - интерфейс методы базавой сущности репозитории для EntityBaseRepository

        //IEntityBaseRepository - jest interfacem funckij dla implementacji funkcji wszystkich modelej (dla Actor, Cinema, Movies i td)
        //Zrobiono to z tego celu, zeby nie powtarzać się wilokrotno. Zeby nie pisać dla kozdego kontrolera te same funkcji 

        //w srodku my zbieramy się opredilit wszystkie metody 



        //GetAllAsync
        Task<IEnumerable<T>> GetAllAsync(); //Zamianiamy typ danych z Actro na T, bo to jest ogolny(obśćyj) funkcje, czyli dla wszystkich



        ///////////////////////////////////////////////////////////////////////////////////////

        /*
        params - приймає змінну кількість аргументів. Тип параметра має бути одновимірним масивом.
         
        Коли ви викликаєте метод із params параметром, ви можете передати:

        - Розділений комами список аргументів типу елементів масиву.
        - Масив аргументів зазначеного типу.
        - Жодних аргументів. Якщо ви не надсилаєте аргументів, довжина списку params дорівнює нулю.

         */


        //GetAllAsync 
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
        ///////////////////////////////////////////////////////////////////////////////////////



        //GetByIdAsync
        Task<T> GetByIdAsync(int id);


        //AddAsync
        Task AddAsync(T entity);


        //UpdateAsync
        Task UpdateAsync(int id, T entity);


        //DeleteAsync
        Task DeleteAsync(int id);
    }
}
