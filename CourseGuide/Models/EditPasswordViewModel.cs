using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CourseGuide.Models
{
    public class EditPasswordViewModel
    {
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z]).{9,}$", ErrorMessage = "Старый пароль должен содержать как строчные, так и заглавные буквы латиницы и иметь длину не менее 9 символов.")]
        [Required(ErrorMessage = "Поле Пароль обязательно для заполнения")]
        [DisplayName("Старый пароль")]
        public string CurrentPassword { get; set; }
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z]).{9,}$", ErrorMessage = "Новый пароль должен содержать как строчные, так и заглавные буквы латиницы и иметь длину не менее 9 символов.")]
        [Required(ErrorMessage = "Поле Пароль обязательно для заполнения")]
        [DisplayName("Новый пароль")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Поле Пароль ещё раз обязательно для заполнения")]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
        [DisplayName("Новый пароль ещё раз")]
        public string ConfirmPassword { get; set; }
    }
}
