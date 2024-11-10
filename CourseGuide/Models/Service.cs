using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace CourseGuide.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название услуги обязательно для заполнения.")]
        [DisplayName("Название")]
        public string ServiceName { get; set; }

        [StringLength(500, ErrorMessage = "Описание услуги не может превышать 500 символов.")]
        [DisplayName("Описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Цена услуги обязательна для заполнения.")]
        [Range(0, double.MaxValue, ErrorMessage = "Цена должна быть неотрицательным числом.")]
        [DisplayName("Цена")]
        public decimal Price { get; set; }

        public int EducationalInstitutionId { get; set; }
        public EducationalInstitution? EducationalInstitution { get; set; }
        // Коллекция заявок
        public ICollection<Applications> Applications { get; set; } = new List<Applications>();

        // Коллекция отзывов
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
