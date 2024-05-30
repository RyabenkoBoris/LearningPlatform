using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.ViewModels
{
    public class SectionViewModel
    {
        [Required(ErrorMessage = "Введіть назву розділу")]
        [MaxLength(100, ErrorMessage = "Максимальна довжина - 100")]
        public string Name { get; set; } = string.Empty;
        [HiddenInput]
        public int Id { get; set; }
        [HiddenInput]
        public int CourseId { get; set; }
    }
}
