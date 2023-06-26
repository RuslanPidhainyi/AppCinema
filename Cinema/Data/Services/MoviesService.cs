using AppCinema.Data.Base;
//using AppCinema.Data.ViewModels;
using AppCinema.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AppCinema.Data.Services
{
    public class MoviesService : EntityBaseRepository<Movie>, IMoviesService
    {
        private readonly AppDbContext _context;
        public MoviesService(AppDbContext context) : base(context)
        {
            _context = context;
        }

       ///////////////////////////////////////////////////////////////////////

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDetails = await _context.Movies
                .Include( e => e.Cinema)
                .Include(e => e.Producer)
                .Include(e => e.Actors_Movies).ThenInclude(e => e.Actor)
                .FirstOrDefaultAsync(e => e.Id == id);

            return movieDetails;
        }

        



    }
}
