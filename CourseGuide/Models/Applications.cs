using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CourseGuide.Models
{
    public class Applications
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Описание заявки обязательно для заполнения.")]
        [StringLength(1000, ErrorMessage = "Описание заявки не может превышать 1000 символов.")]
        [DisplayName("Описание заявки")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Выберите услугу")]
        [DisplayName("Услуга")]
        public int ServiceId { get; set; }
        public Service? Service { get; set; }

        [Required(ErrorMessage = "Укажите номер теелфона")]
        [DisplayName("Номер телефона")]
        [RegularExpression(@"^(?:\+7[0-9]{10}|8[0-9]{10})$", ErrorMessage = "Некорректный номер телефона. Используйте формат +7XXXXXXXXXX или 8XXXXXXXXXX.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Укажите свое имя")]
        [DisplayName("Ваше имя")]
        public string Name { get; set; }

        public string UserId { get; set; }
        public  ApplicationUser? User { get; set; }
    
        public string Status { get; set; }

        // Опционально: можно добавить список для заполнения выпадающего списка
        public List<string> Statuses { get; } = new List<string> { "Новая", "Принята", "Отменена" };
    }

}
