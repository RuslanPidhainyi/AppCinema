using AppCinema.Data;
using AppCinema.Data.Services;
using AppCinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        //Details
        [HttpGet]
        public async Task<IActionResult> Details (int id)
        {
            var movieDetail = await _service.GetMovieByIdAsync( id);
            return View(movieDetail);   
        }



        //Create
        public IActionResult Create () 
        {
            ViewData["Welcome"] = "Welcome to our store";
            ViewBag.Description = "This is the store the description";
            return View(); 
        }


    }
}
