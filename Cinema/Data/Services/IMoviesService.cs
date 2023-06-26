using AppCinema.Data.Base;
//using AppCinema.Data.ViewModels;
using AppCinema.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppCinema.Data.Services
{
    public interface IMoviesService : IEntityBaseRepository<Movie>
    {
        //IEntityBaseRepository - jest interfacem funckij dla implementacji funkcji wszystkich modelej (dla Actor, Cinema, Movies i td)
        //Zrobiono to z tego celu, zeby nie powtarzać się wilokrotno. Zeby nie pisać dla kozdego kontrolera te same funkcji 

        Task<Movie> GetMovieByIdAsync(int id);

        //Task AddNewMovieAsync(NewMovieVM data);

        //Task UpdateMovieAsync(NewMovieVM data);

    }
}
