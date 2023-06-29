using AppCinema.Data;
using AppCinema.Data.Services;
using AppCinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; //for Select which corresponds than List Actor, Cinema, Movie in Routing Movie/Create. This line 65
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AppCinema.Controllers
{
    public class MoviesController : Controller
    {

        private readonly IMoviesService _service;

        public MoviesController(IMoviesService service)
        {
            _service = service;
        }



        /*
        private readonly AppDbContext _context;

        public MoviesController(AppDbContext context)
        {
            _context = context;
        }
        */

        /////////////////////////////////////////////////////////////////////////

        /*
        public async Task<IActionResult> Index()
        {
            var allMovies = await _context.Movies.Include(e => e.Cinema).ToListAsync();
            return View(allMovies);
        }
        */


        //Index
        public async Task<IActionResult> Index()
        {
            var allMovies = await _service.GetAllAsync( e => e.Cinema);            
            return View(allMovies);
        }

        //Filter for search movies
        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await _service.GetAllAsync(e => e.Cinema);

            //що якщо відмінність від рядка є нульовою або порожньою 
            if (!string.IsNullOrEmpty(searchString))
            {
                /*
                 Тоді ми матимемо відфільтровані результати, наші відфільтровані результати дорівнюють всім фільмам,
                 де ми спочатку перевіримо ім’я  або ми перевіримо, чи містить назва пошуковий рядок або опис фільму
                */
                var filteredResult =  allMovies.Where(n => n.Name.Contains(searchString) || n.Description.Contains(searchString)).ToList();

                //повернути представлення, а потім ми визначимо ім’я представлення
                //Отже, перше бомбардування буде виглядом того, що ми повернемося. І друге бомбардування, попадання брудної бомби або модель об’єкта для цього виду. У цьому випадку це буде відфільтрований результат.
                return View("Index", filteredResult);
            }
            //Інакше, якщо у нас немає пошукового рядка, ми просто повернемо всі фільми.
            return View("Index", allMovies);
        }

        //Details
        [HttpGet]
        public async Task<IActionResult> Details (int id)
        {
            var movieDetail = await _service.GetMovieByIdAsync( id);
            return View(movieDetail);   
        }


        //Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var movieDropdownsData = await _service.GetNewMovieDropdownsValues();
    
                                                                //Indyfikator i Tekst ktory urzytkownik moze zobaczyc na liscie 
            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            if(!ModelState.IsValid)
            {
                var movieDropdownsData = await _service.GetNewMovieDropdownsValues();


                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

                return View(movie);
            }

            await _service.AddNewMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }



        //Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var movieDetails = await _service.GetMovieByIdAsync(id);
            
            if(movieDetails == null)
            {
                return View("NotFound");
            }


            var response = new NewMovieVM()
            {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                Price = movieDetails.Price,   
                StartDate = movieDetails.StartDate,
                EndDate = movieDetails.EndDate,
                ImageURL = movieDetails.ImageURL,  
                MovieCategory = movieDetails.MovieCategory,
                CinemaId = movieDetails.CinemaId,   
                ProducerId = movieDetails.ProducerId,
                ActorIds = movieDetails.Actors_Movies.Select(n => n.ActorId).ToList()
            };


            var movieDropdownsData = await _service.GetNewMovieDropdownsValues();

            //Indyfikator i Tekst ktory urzytkownik moze zobaczyc na liscie 
            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            return View(response);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMovieVM movie)
        {
            if(id != movie.Id)
            {
                return View("NotFound"); 
            }

            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _service.GetNewMovieDropdownsValues();


                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

                return View(movie);
            }

            await _service.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }

    }
}
