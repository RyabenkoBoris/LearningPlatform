using LearningPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.ViewModels
{
    public class TeacherClassesViewModel
    {
        [HiddenInput]
        public Guid Id { get; set; }
        [HiddenInput]
        public short lessonNumber { get; set; }
        [HiddenInput]
        public bool isFirstWeek { get; set; }
        [HiddenInput]
        public DayOfWeek DayOfWeek { get; set; }
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Виберіть предмет")]
        public int? LessonId { get; set; }
        public List<LessonEntity> Lessons { get; set; } = [];
        [HiddenInput]
        public int ClassId { get; set; }
        public List<ClassEntity> Classes { get; set; } = [];
        public int ClassTypeId { get; set; }
        public List<ClassTypeEntity> ClassTypes { get; set; } = [];
        public List<string> SheduleSample { get; set; } = [];
    }
}
