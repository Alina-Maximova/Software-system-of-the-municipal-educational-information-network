using CourseGuide.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CourseGuide.Controllers
{
    public class EducationalInstitutionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EducationalInstitutionController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult EducationalInstitutionAll()
        {
            var institutions = _context.EducationalInstitutions.Include(i => i.Services).ToList();
            return PartialView("EducationalInstitutionAll", institutions);
        }
        [HttpGet]
        public async Task<IActionResult> EducationalInstitutionDetails(int id)
        {
            var institutions =  await _context.EducationalInstitutions
                 .Include(r => r.Services)
                 .ThenInclude(s => s.Reviews)
                 .ThenInclude(s => s.User)
                 .FirstOrDefaultAsync(i => i.Id == id);

            return View("EducationalInstitutionDetails", institutions);
        }
        // Метод для отображения списка учебных заведений

        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }

        // Метод для отображения конкретного учебного заведения
        [Authorize]
        [HttpGet]
        public IActionResult Details(int id)
        {
            var institution = _context.EducationalInstitutions.Include(i => i.Services)
                                                  .FirstOrDefault(i => i.Id == id);
            if (institution == null)
            {
                return NotFound();
            }
            return View(institution);
        }

        // Работа с отзывами
        [HttpGet]
        public async Task<IActionResult> CreateReview()
        {
            var services = await _context.Services.Include(i => i.EducationalInstitution).ToListAsync();
            //ViewBag.Services = new SelectList(services, "Id", "Name", "EducationalInstitution.Name");
            ViewData["Services"] = services;

            return PartialView("CreateReview");
        }


        [HttpPost]
        public async Task<IActionResult> CreateReview([FromForm] Review review)
        {
            Console.WriteLine(review.UserId);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == review.UserId);
            review.UserId = user.Id;

            if (!ModelState.IsValid)
            {
                Console.WriteLine(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                     .Select(e => e.ErrorMessage)
                     .ToList();

                return BadRequest(new { success = false, errors });
            }
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            // Instead of returning a view, just return a message
            return Json(new { success = true, message = "Отзыв успешно добавлена" });

        }


        // Работа с отзывами
        [HttpGet]
        public async Task<IActionResult> CreateApplication()
        {
            var services = await _context.Services.Include(i => i.EducationalInstitution).ToListAsync();
            //ViewBag.Services = new SelectList(services, "Id", "Name", "EducationalInstitution.Name");
            ViewData["Services"] = services;

            return PartialView("CreateApplication");
        }


        [HttpPost]
        public async Task<IActionResult> CreateApplication([FromForm] Applications application)
        {
            Console.WriteLine(application.UserId);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == application.UserId);
            application.UserId = user.Id;
            Console.WriteLine(application.UserId);

            if (!ModelState.IsValid)
            {
                Console.WriteLine(ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

                return BadRequest(new { success = false, errors });
            }
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            // Instead of returning a view, just return a message
            return Json(new { success = true, message = "Заявка успешно добавлена" });
        }
      




    }
}
