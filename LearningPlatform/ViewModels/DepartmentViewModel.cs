using LearningPlatform.Models;
using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введіть назву підрозділу")]
        [MaxLength(100, ErrorMessage = "Максимальна довжина - 100")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Введіть абревіатуру підрозділу")]
        [MaxLength(10, ErrorMessage = "Максимальна довжина - 10")]
        public string Abbreviation { get; set; } = string.Empty;
        [Required(ErrorMessage = "Виберіть підрозділ")]
        public int FacultyId { get; set; }
        public List<FacultyEntity> Faculties { get; set; } = [];
    }
}
