using LearningPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.ViewModels
{
    public class DepartamentLessonsViewModel
    {
        [HiddenInput]
        public int DepartamentId { get; set; }
        [HiddenInput]
        public string DepartamentName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Виберіть предмет")]
        public int? LessonId { get; set; }
        public List<LessonEntity> Lessons { get; set; } = [];
    }
}
