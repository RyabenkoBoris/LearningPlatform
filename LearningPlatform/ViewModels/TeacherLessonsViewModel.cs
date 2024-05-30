using LearningPlatform.Models;
using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.ViewModels
{
    public class TeacherLessonsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Виберіть предмет")]
        public int? LessonId { get; set; }
        public List<LessonEntity> Lessons { get; set; } = [];
    }
}
