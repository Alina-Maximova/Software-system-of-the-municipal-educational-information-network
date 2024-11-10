using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CourseGuide.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Поле Логин обязательно для заполнения")]
        [DisplayName("Логин")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Поле Пароль обязательно для заполнения")]
        [DisplayName("Пароль")]
        public string Password { get; set; }
    }
}
