using Microsoft.AspNetCore.Identity;

namespace CourseGuide.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Surname
        {
            get; set;
        }
        public string? Name
        {
            get; set;
        }
        public string? Patronymic
        {
            get; set;
        }
        public virtual ICollection<Applications> Applications { get; set; } = new List<Applications>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    }

}
