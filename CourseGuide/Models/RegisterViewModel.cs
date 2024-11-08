using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CourseGuide.Models
{
    public class RegisterViewModel
    {

        [Required(ErrorMessage = "Поле Имя обязательно для заполнения")]
        [DisplayName("Имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле Логин обязательно для заполнения")]
        [DisplayName("Логин")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Поле E-mail обязательно для заполнения")]
        [EmailAddress(ErrorMessage = "Введен некорректный адрес")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z]).{9,}$", ErrorMessage = "Пароль должен содержать как строчные, так и заглавные буквы латиницы и иметь длину не менее 9 символов.")]
        [Required(ErrorMessage = "Поле Пароль обязательно для заполнения")]
        [DisplayName("Пароль")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Поле Пароль ещё раз обязательно для заполнения")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DisplayName("Пароль ещё раз")]
        public string ConfirmPassword { get; set; }


    }
}