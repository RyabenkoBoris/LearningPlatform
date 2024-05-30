using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.ViewModels
{
    public class LessonViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Введіть назву предмету")]
        [MaxLength(100, ErrorMessage = "Максимальна довжина - 100")]
        public string Name { get; set; } = string.Empty;
    }
}
