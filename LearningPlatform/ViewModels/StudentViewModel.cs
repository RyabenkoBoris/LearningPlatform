using LearningPlatform.Models;
using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.ViewModels
{
    public class StudentViewModel
    {
        public Guid Id { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Введіть Email")]
        public string Email { get; set; } = string.Empty;

        [DataType(DataType.Password, ErrorMessage = "Помилка")]
        [Required(ErrorMessage = "Введіть пароль"), MinLength(5, ErrorMessage = "Мінімальна довжина паролю 5")]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Введіть ПІБ")]
        [MaxLength(100, ErrorMessage = "Максимальна довжина - 100")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Введіть групу")]
        public int? GroupId { get; set; }
        public List<DepartmentEntity> Departments { get; set; } = [];
        public List<FacultyEntity> Faculties { get; set; } = [];
        public List<GroupEntity> Groups { get; set; } = [];
    }
}
