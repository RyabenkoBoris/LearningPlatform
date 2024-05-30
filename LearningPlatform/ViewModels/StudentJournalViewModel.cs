using LearningPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.ViewModels
{
    public class StudentJournalViewModel
    {
        [HiddenInput]
        public Guid StudentId { get; set; }
        [HiddenInput]
        public int JournalId { get; set; }
        [HiddenInput]
        public Guid? TeacherId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Виберіть тип оцінювання")]
        public int? AssesmentTypeId { get; set; }
        public string? Note { get; set; }
        [Range(0, 1000, ErrorMessage = "Діапазон від 0 до 1000")]
        public int Mark { get; set; }
        public int Sum { get; set; }
        public List<GradeEntity> Grades { get; set; } = [];
        public List<AssessmentTypeEntity> AssesmentTypes { get; set; } = [];
    }
}
