using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CourseGuide.Models
{
    public class EducationalInstitution
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название учебного заведения обязательно для заполнения.")]
        [StringLength(100, ErrorMessage = "Название не может превышать 100 символов.")]
        [DisplayName("Название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Адрес учебного заведения обязателен для заполнения.")]
        [StringLength(200, ErrorMessage = "Адрес не может превышать 200 символов.")]
        [DisplayName("Адрес")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Описание учебного заведения обязателен для заполнения.")]
        [StringLength(500, ErrorMessage = "Описание услуги не может превышать 500 символов.")]
        [DisplayName("Описание")]
        public string Description { get; set; } = null;
        

        public ICollection<Service> Services { get; set; } = new List<Service>(); // Связь с услугами
        public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();

    }
}