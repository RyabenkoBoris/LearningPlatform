using LearningPlatform.Models;
using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.ViewModels
{
    public class GroupViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введіть назву групи")]
        [MaxLength(100, ErrorMessage = "Максимальна довжина - 100")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Виберіть факультет")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Виберіть спеціальність")]
        public int SpecialityId { get; set; }
        public List<DepartmentEntity> Departments { get; set; } = [];
        public List<SpecialityEntity> Specialities { get; set; } = [];
        public List<FacultyEntity> Faculties { get; set; } = [];
    }
}
