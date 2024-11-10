using CourseGuide.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseGuide.Controllers
{
    public class EducationalInstitutionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EducationalInstitutionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Метод для отображения списка учебных заведений
        [HttpGet]
        public IActionResult Index()
        {
            var institutions = _context.EducationalInstitutions.Include(i => i.Services).ToList();
            return View(institutions);
        }

        // Метод для отображения конкретного учебного заведения
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

        // Метод для добавления нового учебного заведения
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EducationalInstitution institution)
        {
            if (ModelState.IsValid)
            {
                _context.EducationalInstitutions.Add(institution);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(institution);
        }

        // Метод для добавления услуги к учебному заведению
        [HttpGet]
        public IActionResult AddService(int institutionId)
        {
            var service = new Service { EducationalInstitutionId = institutionId };
            return View(service);
        }

        [HttpPost]
        public IActionResult AddService(Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Services.Add(service);
                _context.SaveChanges();
                return RedirectToAction(nameof(Details), new { id = service.EducationalInstitutionId });
            }
            return View(service);
        }
    }
}
