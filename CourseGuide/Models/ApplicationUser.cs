using Microsoft.AspNetCore.Identity;

namespace CourseGuide.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name
        {
            get; set;
        }
    }
    
}
