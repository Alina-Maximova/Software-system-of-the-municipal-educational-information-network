using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CourseGuide.Models;
using Microsoft.AspNetCore.Identity;

namespace CourseGuide.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
        {
            var userWithSameUserName = await _userManager.FindByNameAsync(model.UserName);
            var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);

            if (userWithSameUserName != null && userWithSameEmail != null )
            {
                ModelState.AddModelError("UserName", "Пользователь с таким логином и почтой уже существует.");
                return View("Register", model);
            }
            else { 

            if (userWithSameUserName != null)
            {
                ModelState.AddModelError("UserName", "Пользователь с таким логином уже существует.");
                return View("Register", model);
            }
            if (userWithSameEmail != null)
                {
                    ModelState.AddModelError("Email", "Пользователь с такой почтой уже существует.");
                    return View("Register", model);
                }
            }

           
            
            var user = new ApplicationUser
            {
                Name = model.Name,
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password!);
            Console.WriteLine(result);
            if (result.Succeeded)
            {
               Console.WriteLine( await _userManager.AddToRoleAsync(user, "User"));
                await _signInManager.SignInAsync(user, false);

                var user1 = await _userManager.FindByNameAsync(model.UserName!);

               

                    return RedirectToAction("Home");
               
            }
            return BadRequest("Что-то пошло нитак :(");

        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.UserName!, model.Password!, false, false);
                if (result.Succeeded)
                {
                  
                        return RedirectToAction("Index", "Home");
                 
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}