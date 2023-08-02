using AppCinema.Data;
using AppCinema.Data.Services;
using AppCinema.Data.Static;
using AppCinema.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AppCinema.Controllers
{
    /*
      [Authorize] - Якщо ви хочете обмежити доступ усіх користувачів до будь-якого API та зв’язку чи будь-якої дії з цього контролер
                    Якщо ви хочете отримати доступ до будь-якої дії тут, вам потрібно авторизуватися.

                І оскільки ми не визначили жодних ролей тут, перевірятиметься лише те,
                чи ви ввійшли в систему або ні.
   */

    [Authorize(Roles = UserRoles.Admin)]
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


        /*
         [AllowAnonymous] - щоб дозволити автентифікованим користувачам або дозволити анонімним, наприклад, перерахування всіх Actors 
        таким же чином, коли ви не ввійшли в систему.
        */
        //Index
        [AllowAnonymous]
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
        [AllowAnonymous]
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
