using CourseGuide.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace CourseGuide.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;

        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel model)
        {
            Console.WriteLine(model.ToString());
            var userWithSameUserName = await _userManager.FindByNameAsync(model.UserName);
            var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);

            if (userWithSameUserName != null && userWithSameEmail != null)
            {
                ModelState.AddModelError("UserName", "Пользователь с таким логином и почтой уже существует.");
                return View("Register", model);
            }
            else
            {

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
                Surname = model.Surname,
                Name = model.Name,
                Patronymic = model.Patronymic,
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password!);
            Console.WriteLine(result);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
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

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Account()
        {
            return View();
        }

        // Работа с заявками
        [HttpGet]
        public async Task<IActionResult> ApplicationAllUser()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
            {
                return NotFound("User not found" + User + ",mn");
            }

            var applications = await _context.Applications
                .Include(r => r.Service)
                    .ThenInclude(s => s.EducationalInstitution)
                .Include(r => r.User)
                .Where(r => r.UserId == user.Id)
                .ToListAsync();

            return PartialView("ApplicationAllUser", applications);
        }

        [HttpGet]
        public async Task<IActionResult> ApplicationsCancel(int id)
        {
            var applications = await _context.Applications
                .Include(r => r.Service)
                .ThenInclude(s => s.EducationalInstitution)
                .Include(r => r.User)
                .FirstOrDefaultAsync(i => i.Id == id);


            if (applications != null)
            {
                applications.Status = "Отменена";
                _context.Applications.Update(applications);
                _context.SaveChanges();
                ViewData["SuccessMessage"] = "Вы отменили заявку";


                return View("Account");
            }

            return View("Account", "Account");

        }

        [HttpGet]

        public async Task<IActionResult> EditProf()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var model = new EditProfileViewModel
            {
                Surname = user.Surname,
                Name = user.Name,
                Patronymic = user.Patronymic,
                UserName = user.UserName,
                Email = user.Email
            };
            return PartialView("EditProf", model);
        }
        [HttpPost]
        public async Task<IActionResult> EditProf1([FromForm] EditProfileViewModel model)
        {
            Console.WriteLine(ModelState.IsValid);
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                                .Select(e => e.ErrorMessage)
                                                .ToList();

                return BadRequest(new { success = false, errors });
            }
            Console.WriteLine(User.Identity.Name);
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user != null)
            {
                if (user.UserName != model.UserName && user.Email != model.Email)
                {
                    var userWithSameUserName = await _userManager.FindByNameAsync(model.UserName);

                    var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);
                    if (userWithSameUserName != null && userWithSameEmail != null)
                    {
                        return BadRequest(new { success = false, errors = "Пользователь с таким логином и почтой уже существует." });

                    }
                    else
                    {

                        if (userWithSameUserName != null)
                        {
                            return BadRequest(new { success = false, errors = "Пользователь с таким логином уже существует." });

                        }
                        if (userWithSameEmail != null)
                        {
                            return BadRequest(new { success = false, errors = "Пользователь с такой почтой уже существует." });


                        }
                    }
                }
                else
                {
                    if (user.UserName != model.UserName)
                    {
                        var userWithSameUserName = await _userManager.FindByNameAsync(model.UserName);

                        if (userWithSameUserName != null)
                        {
                            return BadRequest(new { success = false, errors = "Пользователь с такой логином уже существует." });

                        }
                    }
                    if (user.Email != model.Email)
                    {
                        var userWithSameEmail = await _userManager.FindByNameAsync(model.Email);

                        if (userWithSameEmail != null)
                        {
                            return BadRequest(new { success = false, errors = "Пользователь с такой логином уже существует." });

                        }
                    }
                }

                user.Surname = model.Surname;
                user.Name = model.Name;
                user.Patronymic = model.Patronymic;
                user.UserName = model.UserName;
                user.Email = model.Email;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    //Console.WriteLine(await _userManager.AddToRoleAsync(user, "User"));
                    await _signInManager.SignInAsync(user, false);
                    // Обработка успешного обновления, например перенаправление на страницу профиля или результат успеха
                    return Json(new { success = true, message = "Вы изменили данные" });

                }
                return BadRequest(new { success = false, errors = "Пользователя нет" });


            }



            return BadRequest(new { success = false, errors = "Что-то пошло нитак :()" });

        }
        [HttpGet]
        public async Task<IActionResult> EditPassword()
        {
            return PartialView("EditPassword");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPassword([FromForm] EditPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                 .Select(e => e.ErrorMessage)
                                 .ToList();

                return BadRequest(new { success = false, errors }); ;

            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest(new { success = false, errors = "Пользователя нет" });

            }

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Json(new { success = true, message = "Вы изменили пароль данные" });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return BadRequest(new { success = false, errors = "Старый пароль неверный" });

        }
        [HttpGet]

        public async Task<IActionResult> DeleteAccount()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return BadRequest(new { success = false, errors = "Пользователя нет" });

            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                await _signInManager.SignOutAsync(); // Выйти из системы после удаления
                return RedirectToAction("Index", "Home"); // Перенаправление на главную страницу
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors)
                                            .Select(e => e.ErrorMessage)
                                            .ToList();

            return BadRequest(new { success = false, errors });
        }

        [HttpGet]
        [Authorize(Roles = "Employee")]

        public async Task<IActionResult> ApplicationAllEd()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);


            var applications = await _context.Applications
           .Include(r => r.Service)
               .ThenInclude(s => s.EducationalInstitution)
           .Include(r => r.User)
           .Where(s => s.Service.EducationalInstitutionId == user.EducationalInstitutionId)
           .ToListAsync();

            return PartialView("ApplicationAllEd", applications);


        }
        [HttpGet]
        public async Task<IActionResult> ApplicationsAccept(int id)
        {
            var applications = await _context.Applications
                .Include(r => r.Service)
                .ThenInclude(s => s.EducationalInstitution)
                .Include(r => r.User)
                .FirstOrDefaultAsync(i => i.Id == id);


            if (applications != null)
            {
                applications.Status = "Принята";
                _context.Applications.Update(applications);
                _context.SaveChanges();
                ViewData["SuccessMessage"] = "Вы приняли заявку";


                return View("Account");
            }

            return View("Account");

        }
        [HttpGet]
        public IActionResult ReportCreate()
        {
            return PartialView("ReportCreate");
        }
        [HttpPost]
        public async Task<IActionResult> ReportCreate([FromForm] Report report)
        {
           
            Console.WriteLine(report.AcademicYear);
            if (!ModelState.IsValid)
            {
                return View("Account", report);

            }
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            report.EducationalInstitutionId = user.EducationalInstitutionId;
            report.Status = "Новый";
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

            ViewData["SuccessMessage"] = "Учебное заведение успешно создано!";
            return View("Account");

        }
    }

}


