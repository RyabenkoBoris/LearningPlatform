using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.ViewModels
{
    public class TaskViewModel
    {
        [Required(ErrorMessage = "Введіть назву завдання")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Введіть кількість балів")]
        [Range(0, 1000, ErrorMessage = "Діапазон балів від 0 до 1000")]
        public short MaxMark { get; set; }
        public string TaskText { get; set; } = string.Empty;
        [HiddenInput]
        public int SectionId { get; set; }
        [HiddenInput]
        public int CourseId { get; set; }
        [HiddenInput]
        public int TaskId { get; set; }
        public DateOnly DueDate { get; set; }
        public TimeOnly DueTime { get; set; }
    }
}
