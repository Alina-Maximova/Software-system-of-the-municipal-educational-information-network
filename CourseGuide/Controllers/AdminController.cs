using CourseGuide.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CourseGuide.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;


        public AdminController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;

        }

        [HttpGet]
        public IActionResult Admin()
        {
            return View();
        }

        //Работа с пользователями
        [HttpGet]
        public async Task<IActionResult> UserAll()
        {
            var users = _userManager.Users.ToList();
            var userWithRoles = new List<UserReg>();



            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                Console.WriteLine(string.Join(", ", user));
                userWithRoles.Add(new UserReg
                {
                    Surname = user.Surname,
                    Name = user.Name,
                    Patronymic = user.Patronymic,
                    UserName = user.UserName,
                    Email = user.Email,
                    UserRole = roles[0]
                });
            }

            return PartialView("UserAll", userWithRoles);
        }

        [HttpGet]
        public IActionResult UserCreate()
        {
            return PartialView("UserCreate");
        }

        [HttpPost]
        public async Task<IActionResult> UserCreate([FromForm] UserReg model)
        {
            var userWithSameUserName = await _userManager.FindByNameAsync(model.UserName);
            var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);

            if (userWithSameUserName != null && userWithSameEmail != null)
            {
                ModelState.AddModelError("UserName", "Пользователь с таким логином и почтой уже существует.");
                return View("Admin", model);

            }
            else
            {

                if (userWithSameUserName != null)
                {
                    ModelState.AddModelError("UserName", "Пользователь с таким логином уже существует.");
                    return View("Admin", model);

                }
                if (userWithSameEmail != null)
                {
                    ModelState.AddModelError("Email", "Пользователь с такой почтой уже существует.");
                    return View("Admin", model);
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
            Console.WriteLine(model.UserRole);
            if (result.Succeeded)
            {
                Console.WriteLine(await _userManager.AddToRoleAsync(user, model.UserRole));

                ViewData["SuccessMessage"] = "Пользователь успешно создан!";

                return View("Admin");

            }
            return BadRequest("Что-то пошло нитак :(");

        }
        [HttpGet]
        public async Task<IActionResult> UserDelete(string UserName)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(UserName);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
                ViewData["SuccessMessage"] = "Вы удалили пользователя " + user.UserName;


                return View("Admin");

            }
            return View("Admin");

        }


        //Работа с учебными заведениями
        [HttpGet]
        public IActionResult EducationalInstitutionAll()
        {
            var institutions = _context.EducationalInstitutions.Include(i => i.Services).ToList();
            return PartialView("EducationalInstitutionAll", institutions);
        }
        public IActionResult EducationalInstitutionCreate()
        {
            return PartialView("EducationalInstitutionCreate");
        }
        [HttpPost]
        public IActionResult EducationalInstitutionCreate([FromForm] EducationalInstitution institution)
        {
            if (ModelState.IsValid)
            {
                _context.EducationalInstitutions.Add(institution);
                _context.SaveChanges();
                ViewData["SuccessMessage"] = "Учебное заведение успешно создано!";
                Console.WriteLine(ModelState);
                return View("Admin");
            }
            Console.WriteLine(ModelState.IsValid);
            return View("Admin", institution);


        }
        [HttpGet]
        public async Task<IActionResult> EducationalInstitutionDelete(int id)
        {
            var institution = await _context.EducationalInstitutions
                .Include(i => i.Services) // Загружаем связанные услуги
                .FirstOrDefaultAsync(i => i.Id == id);


            if (institution != null)
            {
                _context.EducationalInstitutions.Remove(institution);
                _context.SaveChanges();
                ViewData["SuccessMessage"] = "Вы удалили " + institution.Name;


                return View("Admin");
            }

            return View("Admin");

        }
        [HttpGet]
        public IActionResult EducationalInstitutionUpdate(int id)
        {
            Console.WriteLine($"EducationalInstitution {id}");
            var institution = _context.EducationalInstitutions.Find(id);
            if (institution == null)
            {
                return NotFound();
            }
            Console.WriteLine($"EducationalInstitution {institution}");

            return PartialView("EducationalInstitutionUpdate", institution);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EducationalInstitutionUpdatePost([FromForm] EducationalInstitution institution)
        {
            if (ModelState.IsValid)
            {
                _context.EducationalInstitutions.Update(institution);
                await _context.SaveChangesAsync();

                // Instead of returning a view, just return a message
                return Content($"Вы успешно обновили данные об учебном заведение: {institution.Name}");
            }

            // If model state is invalid, you may want to return the model back to the form
            return BadRequest("Invalid model state");
        }

        // Работа с услугами
        [HttpGet]
        public async Task<IActionResult> ServiceCreate()
        {
            var institutions = await _context.EducationalInstitutions.Include(i => i.Services).ToListAsync();
            ViewBag.EducationalInstitutions = new SelectList(institutions, "Id", "Name");

            return PartialView("ServiceCreate");
        }


        [HttpPost]

        public async Task<IActionResult> ServiceCreate([FromForm] Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Services.Add(service);
                await _context.SaveChangesAsync();

                // Instead of returning a view, just return a message
                return Content($"Вы успешо добавили услугу");
            }
            Console.WriteLine(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            // If model state is invalid, you may want to return the model back to the form
            return BadRequest(new { errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });


        }
        [HttpGet]
        public IActionResult ServiceAll()
        {
            var services = _context.Services
                .Include(i => i.EducationalInstitution)
                .Include(i => i.Reviews)
                .Include(i => i.Applications)
                .ToList();
            return PartialView("ServiceAll", services);
        }
        [HttpGet]
        public async Task<IActionResult> ServiceDelete(int id)
        {
            var services = await _context.Services
                .Include(i => i.EducationalInstitution)
                .FirstOrDefaultAsync(i => i.Id == id);


            if (services != null)
            {
                _context.Services.Remove(services);
                _context.SaveChanges();
                ViewData["SuccessMessage"] = "Вы удалили " + services.ServiceName;


                return View("Admin");
            }

            return View("Admin");

        }
        [HttpGet]
        public IActionResult ServiceUpdate(int id)
        {
            Console.WriteLine($"Service {id}");
            var service = _context.Services
                .Include(i => i.EducationalInstitution)
                .FirstOrDefault(i => i.Id == id);
            if (service == null)
            {
                return NotFound();
            }



            return PartialView("ServiceUpdate", service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ServiceUpdatePost([FromForm] Service service)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine($"service {service.EducationalInstitutionId}");
                Console.WriteLine($"service {service.EducationalInstitution}");

                _context.Services.Update(service);
                _context.SaveChanges();

                // Instead of returning a view, just return a message
                return Content($"Вы успешно обновили данные об учебном заведение: {service.ServiceName}");
            }

            // If model state is invalid, you may want to return the model back to the form
            return BadRequest("Invalid model state");


        }
        //Работа с отзывами
        [HttpGet]
        public IActionResult ReviewAll()
        {
            var reviews = _context.Reviews
                .Include(r => r.Service) 
                .ThenInclude(s => s.EducationalInstitution) 
                .Include(r => r.User) 
                .ToList();

            Console.WriteLine(reviews.Count);
            return PartialView("ReviewAll", reviews);
        }
        [HttpGet]
        public async Task<IActionResult> ReviewDelete(int id)
        {
            var reviews = await _context.Reviews
                .Include(r => r.Service)
                .ThenInclude(s => s.EducationalInstitution)
                .Include(r => r.User)
                .FirstOrDefaultAsync(i => i.Id == id);


            if (reviews != null)
            {
                _context.Reviews.Remove(reviews);
                _context.SaveChanges();
                ViewData["SuccessMessage"] = "Вы удалили отзыв";


                return View("Admin");
            }

            return View("Admin");

        }

        [HttpGet]
        public async Task<IActionResult> ReviewAccept(int id)
        {
            var reviews = await _context.Reviews
                .Include(r => r.Service)
                .ThenInclude(s => s.EducationalInstitution)
                .Include(r => r.User)
                .FirstOrDefaultAsync(i => i.Id == id);


            if (reviews != null)
            {
                reviews.Status = "Принят";
                _context.Reviews.Update(reviews);
                _context.SaveChanges();
                ViewData["SuccessMessage"] = "Вы приняли отзыв";


                return View("Admin");
            }

            return View("Admin");

        }

    }
}
