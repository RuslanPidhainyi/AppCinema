using AppCinema.Data;
using AppCinema.Data.Services;
using AppCinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AppCinema.Controllers
{
    public class ProducersController : Controller
    {

        private readonly IProducersService _service;

        //powączenia serwisuw 
        public ProducersController(IProducersService service)
        {
            _service = service;
        }

        /*

        private readonly AppDbContext _context;

        public ProducersController(AppDbContext context)
        {
            _context = context;
        }
        */


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /*
        //Asynchronny metod - mowi nam poczekaj na tą metode, poki wyswietli nam to co potzrebno,a w tym momencie wykona nam inny processory
        public async Task<IActionResult> Index()
        {
            //Asynchronny metod 
            //Poczekaj poki wyswietli nam wszystkich Produserów z Listy
            var allProducers = await _context.Producers.ToListAsync();

            //Jezeli chesz okresli imie
            //return View("IndexNew", allProducers);

            return View(allProducers);
        }
        */



        //Index
        public async Task<IActionResult> Index()
        {

            var allProducers = await _service.GetAllAsync();
            return View(allProducers);
        }

        //Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName, ProfilePictureURL,Bio")] Producer producer) //[Bind()] - валідація яка звязує властивості(свойства )
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            else
            {
                await _service.AddAsync(producer);
                return RedirectToAction(nameof(Index));
            }
        }

        //Details
        public async Task<IActionResult> Details(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);

            if (producerDetails == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(producerDetails);
            }
        }


        //Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);

            if (producerDetails == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(producerDetails);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName, ProfilePictureURL,Bio")] Producer producer) //[Bind()] - валідація яка звязує властивості(свойства )
        {
            if (!ModelState.IsValid)
            {
                return View(producer);
            }
            else
            {
                await _service.UpdateAsync(id, producer);
                return RedirectToAction(nameof(Index));
            }
        }




        //Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);

            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);

        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));

        }
    }
}
