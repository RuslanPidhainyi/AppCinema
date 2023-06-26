using AppCinema.Data;
using AppCinema.Data.Services;
using AppCinema.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AppCinema.Controllers
{
    public class ActorsController : Controller
    {
        //prywatny odczyt  
        private readonly IActorsService _service;

        //powączenia serwisuw 
        public ActorsController (IActorsService service)
        {
            _service = service;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //Synchronny metod
        /*
        public IActionResult Index()
        {
            //wyswietli nam wszystkich Actorów z Listy
            var data = _context.Actors.ToList();
            return View();
        }
        */


        //Index
        public async Task< IActionResult> Index()
        {

            var data =  await _service.GetAllAsync();
            return View(data);
        }


        //Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("FullName, ProfilePictureURL,Bio")]Actor actor) //[Bind()] - валідація яка звязує властивості(свойства )
        {
            if(!ModelState.IsValid)
            {
                return View(actor);
            }
            else
            {
                await _service.AddAsync(actor);
                return RedirectToAction(nameof(Index));
            }
        }

        //Details
        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);

            if(actorDetails == null)
            {
                return View("NotFound");      
            }
            else
            {
                return View(actorDetails);
            }
        }


        //Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);

            if (actorDetails == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(actorDetails);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName, ProfilePictureURL,Bio")] Actor actor) //[Bind()] - валідація яка звязує властивості(свойства )
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            else
            {
                await _service.UpdateAsync(id, actor);
                return RedirectToAction(nameof(Index));
            }
        }




        //Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);

            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
            
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id) 
        {
            var actorDetails = await _service.GetByIdAsync(id);

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
            
        }
        
    }
}
