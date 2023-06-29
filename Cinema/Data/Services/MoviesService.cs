using AppCinema.Data.Base;
using AppCinema.Data.ViewModels;
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



        public async Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues()
        {
            //response -  дорівнює новому спадному списку нових фільмів, View Model
            var response = new NewMovieDropdownsVM()
            {
                //Zbieramy aktorow i grupujemy po imieniu i nazwisku 
                Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync(),
                Producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync()
            };

            return response;
        }


        public async Task AddNewMovieAsync(NewMovieVM data)
        {
            var NewMovie = new Movie()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,   
                CinemaId = data.CinemaId,
                StartDate = data.StartDate, 
                EndDate = data.EndDate,
                MovieCategory = data.MovieCategory,
                ProducerId = data.ProducerId 
            };
            await _context.Movies.AddAsync(NewMovie);
            await _context.SaveChangesAsync();
            
            //Add Movie Actros
            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = NewMovie.Id,
                    ActorId = actorId
                };

                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMovieAsync(NewMovieVM data)
        {
            var dbMovie = await _context.Movies.FirstOrDefaultAsync( n => n.Id == data.Id);  
            
            if(dbMovie != null)
            {

                dbMovie.Name = data.Name;
                dbMovie.Description = data.Description;
                dbMovie.Price = data.Price;
                dbMovie.ImageURL = data.ImageURL;
                dbMovie.CinemaId = data.CinemaId;
                dbMovie.StartDate = data.StartDate;
                dbMovie.EndDate = data.EndDate;
                dbMovie.MovieCategory = data.MovieCategory;
                dbMovie.ProducerId = data.ProducerId;
               
                await _context.SaveChangesAsync();
            }

            //Removie existion actors
            var exestingActorsDb = _context.Actors_Movies.Where(n => n.MovieId == data.Id).ToList();
             _context.Actors_Movies.RemoveRange(exestingActorsDb); 
            await _context.SaveChangesAsync();  

            //Add Movie Actros
            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = data.Id,
                    ActorId = actorId
                };

                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }
    }
}
