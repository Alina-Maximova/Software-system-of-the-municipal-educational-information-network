using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace CourseGuide.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Заголовок отзыва обязателен для заполнения.")]
        [StringLength(200, ErrorMessage = "Заголовок не может превышать 200 символов.")]
        [DisplayName("Заголовок отзыва")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Текст отзыва обязателен для заполнения.")]
        [StringLength(1000, ErrorMessage = "Текст отзыва не может превышать 1000 символов.")]
        [DisplayName("Текст отзыва")]
        public string Content { get; set; }

        [DisplayName("Рейтинг")]
        [Range(1, 5, ErrorMessage = "Рейтинг должен быть от 1 до 5.")]
        public int Rating { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;
      
        [DisplayName("Услуга")]
        public int ServiceId { get; set; }
        public Service? Service { get; set; }

        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }
        public string Status { get; set; }

        // Опционально: можно добавить список для заполнения выпадающего списка
        public List<string> Statuses { get; } = new List<string> { "Новый", "Принят"};
    }

}
