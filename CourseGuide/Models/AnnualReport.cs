using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CourseGuide.Models
{
    public class AnnualReport
    {
        public int Id { get; set; }


        public int? EducationalInstitutionId { get; set; }        // ID учебного заведения

        public EducationalInstitution? EducationalInstitution { get; set; }
        [Required(ErrorMessage = "Учебный год обязателен для заполнения.")]
        [RegularExpression(@"^\d{4}-\d{4}", ErrorMessage = "Некорректный год. Используйте формат XXXX-XXXX")]
        [DisplayName("Учебный год")]
        public string AcademicYear { get; set; } // Учебный год

        [Required(ErrorMessage = "Поле 'Сколько поступило' обязательно для заполения")]
        [DisplayName("Сколько поступило")]
        public int StudentsAdmitted { get; set; }      // Сколько поступило

        [Required(ErrorMessage = "Поле 'Сколько выпустилось' обязательно для заполения")]
        [DisplayName("Сколько выпустилось")]
        public int StudentsGraduated { get; set; }     // Сколько выпустилось

        [Required(ErrorMessage = "Поле 'Количество преподавателей' обязательно для заполения")]
        [DisplayName("Количество преподавателей")]
        public int NumberOfTeachers { get; set; }      // Количество преподавателей

        [Required(ErrorMessage = "Поле 'Прибыль' обязательно для заполения")]
        [DisplayName("Прибыль")]
        public int Revenue { get; set; }           // Прибыль

        [Required(ErrorMessage = "Поле 'Сколько бесплатных занятий' обязательно для заполения")]
        [DisplayName("Сколько бесплатных занятий")]
        public int FreeClasses { get; set; }           // Сколько бесплатных занятий

        [Required(ErrorMessage = "Поле 'Возрастная группа' обязательно для заполения")]
        [DisplayName("Возрастная группа")]
        public string AgeGroup { get; set; }           // Возрастная группа

        [Required(ErrorMessage = "Поле 'Планы на следующий год' обязательно для заполения")]
        [DisplayName("Планы на следующий год")]
        public string PlansNextYear { get; set; }      // Планы на следующий год

        [Required(ErrorMessage = "Поле 'Трудности' обязательно для заполения")]
        [DisplayName("Трудности")]
        public string Challenges { get; set; }         // Трудности

        public string? Status { get; set; }
        public List<string> Statuses { get; } = new List<string> { "Новый", "Принят", "На доработку" };

        public string? Reason { get; set; }
    }
}
