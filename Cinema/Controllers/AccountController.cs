using AppCinema.Data;
using AppCinema.Data.Static;
using AppCinema.Data.ViewModels;
using AppCinema.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using System.Threading.Tasks;

namespace AppCinema.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users); 
        }
       
        public IActionResult Login() => View(new LoginVM());

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if(!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if(user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if(passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    
                    if(result.Succeeded)
                    {
                        return RedirectToAction("Index", "Movies");
                    }
                }
                TempData["Error"] = "Wrong credentials. Please, try again";//Отже, неправильні облікові дані, спробуйте ще раз.
                return View(loginVM);
            }

            /*
             Інакше тут на екрані з’явиться повідомлення про помилку, яке є одним із прикладів цього 
             неправильні облікові дані для цього користувача не існують.
            */

            /*
              Отже, для цього я просто введу дані TempData. 
            
              У нас є дані перегляду. У нас також є дані про тимчасовий режим.

              Тож для тимчасових даних і для тимчасових даних ми встановимо помилку(Error).
              
              Отже, помилка(Error) буде ключовою, а повідомлення буде неправильним.  
             */
            TempData["Error"] = "Wrong credentials. Please, try again";//Отже, неправильні облікові дані, спробуйте ще раз.
            return View(loginVM);
        }

        public IActionResult Register() => View(new RegisterVM());

        /*Register for the simple users */
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if(!ModelState.IsValid) return View(registerVM);

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerVM);
            }

            var newUser = new ApplicationUser()
            {
                FullName = registerVM.FullName,
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            }


            return View("RegisterCompleted");
        }

        /*Log out for  simple users */
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await  _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movies");
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }
    }
}
