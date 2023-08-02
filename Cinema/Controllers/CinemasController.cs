using AppCinema.Data;
using AppCinema.Data.Services;
using AppCinema.Data.Static;
using AppCinema.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AppCinema.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CinemasController : Controller
    {
        private readonly  ICinemasService _service;

        public CinemasController(ICinemasService service)
        {
            _service = service;
        }

        /*
        private readonly AppDbContext _context;

        public CinemasController(AppDbContext context)
        {
            _context = context;
        }
        */

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /*
        public async Task<IActionResult> Index()
        {
            var allCinemas = await _context.Cinemas.ToListAsync();
            return View(allCinemas);
        }
        */



        //Index
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {

            var allCinemas = await _service.GetAllAsync();
            return View(allCinemas);
        }

        //Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name, Logo, Description")] Cinema cinema) //[Bind()] - валідація яка звязує властивості(свойства )
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }
            else
            {
                await _service.AddAsync(cinema);
                return RedirectToAction(nameof(Index));
            }
        }

        //Details
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);

            if (cinemaDetails == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(cinemaDetails);
            }
        }


        //Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);

            if (cinemaDetails == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(cinemaDetails);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name, Logo, Description")] Cinema cinema) //[Bind()] - валідація яка звязує властивості(свойства )
        {
            if (!ModelState.IsValid)
            {
                return View(cinema);
            }
            else
            {
                await _service.UpdateAsync(id, cinema);
                return RedirectToAction(nameof(Index));
            }
        }




        //Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);

            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);

        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var cinemaDetails = await _service.GetByIdAsync(id);

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));

        }


    }
}
