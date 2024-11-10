using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CourseGuide.Models
{
    public class EditProfileViewModel
    {
        [RegularExpression(@"^[А-Яа-яЁё]+(-[А-Яа-яЁё]+)?(\s[А-Яа-яЁё]+)*$", ErrorMessage = "Фамилия должена содержать как строчные, так и заглавные буквы кирилицы.")]
        [Required(ErrorMessage = "Поле Фамилия обязательно для заполнения")]
        [DisplayName("Фамилия")]
        public string Surname { get; set; }

        [RegularExpression(@"^[А-Яа-яЁё]+(-[А-Яа-яЁё]+)?(\s[А-Яа-яЁё]+)*$", ErrorMessage = "Имя должено содержать как строчные, так и заглавные буквы кирилицы.")]
        [Required(ErrorMessage = "Поле Имя обязательно для заполнения")]
        [DisplayName("Имя")]
        public string Name { get; set; }

        [RegularExpression(@"^[А-Яа-яЁё]+(-[А-Яа-яЁё]+)?(\s[А-Яа-яЁё]+)*$", ErrorMessage = "Отчетво должено содержать как строчные, так и заглавные буквы кирилицы.")]
        [DisplayName("Отчетво")]
        public string? Patronymic { get; set; }

        [Required(ErrorMessage = "Поле Логин обязательно для заполнения")]
        [DisplayName("Логин")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Поле E-mail обязательно для заполнения")]
        [EmailAddress(ErrorMessage = "Введен некорректный адрес")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

    }
}
