using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CourseGuide.Models
{
    public class UserReg
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
        public string Patronymic { get; set; }

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


        // Новое свойство для выпадающего списка
        [Required(ErrorMessage = "Выберите роль пользователя")]
        [DisplayName("Роль")]
        public string UserRole { get; set; }

        // Опционально: можно добавить список для заполнения выпадающего списка
        public List<string> UserRoles { get; } = new List<string> { "Employee", "Admin" };
        public int? EducationalInstitutionId { get; set; }
        public virtual EducationalInstitution? EducationalInstitution { get; set; }
    }


}
