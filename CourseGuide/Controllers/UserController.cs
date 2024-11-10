using CourseGuide.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourseGuide.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;


        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]


        [HttpGet]
        public IActionResult UserCreate()
        {
            return PartialView("UserCreate");
        }



        [HttpGet]
        public async Task<IActionResult> UserAll()
        {
            var users = _userManager.Users.ToList();
            var userWithRoles = new List<UserReg>();
            var userWithRoles1 = new List<UserReg>();


            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                Console.WriteLine(string.Join(", ", user));
                userWithRoles.Add(new UserReg
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    UserRole = roles[0]
                });
            }

            return PartialView("UserAll", userWithRoles);
        }
    }
}


