using LearningPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.ViewModels
{
    public class CourseViewModel
    {
        [Required(ErrorMessage = "Введіть назву курсу")]
        [MaxLength(100, ErrorMessage = "Максимальна довжина - 100")]
        public string Name { get; set; } = string.Empty;
        [HiddenInput]
        public int CourseId { get; set; }
        [HiddenInput]
        public Guid UserId { get; set; }
        public List<CourseEntity> Courses { get; set; } = [];
    }
}
