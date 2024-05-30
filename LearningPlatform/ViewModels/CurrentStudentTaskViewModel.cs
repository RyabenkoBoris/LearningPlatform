using LearningPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.ViewModels
{
    public class CurrentStudentTaskViewModel
    {
        [HiddenInput]
        public int TaskId { get; set; }
        [HiddenInput]
        public int StudentTaskId { get; set; }
        public string Comment { get; set; } = string.Empty;
        public List<FilePathEntity> StudentFiles { get; set; } = [];
        public List<CommentEntity> Comments { get; set; } = [];
        public string StudentName { get; set; } = string.Empty;
        public string TaskName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Введіть кількість балів")]
        [Range(0, 1000, ErrorMessage = "Діапазон балів від 1 до 1000")]
        public int Mark { get; set; }
        public int MaxMark { get; set; }
    }
}
