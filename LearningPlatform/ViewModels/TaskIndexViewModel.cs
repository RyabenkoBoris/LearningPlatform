using LearningPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace LearningPlatform.ViewModels
{
    public class TaskIndexViewModel
    {
        [HiddenInput]
        public int StudentTaskId { get; set; }
        [HiddenInput]
        public int TaskId { get; set; }
        [HiddenInput]
        public int Id { get; set; }
        public int MaxMark { get; set; }
        public int Mark { get; set; }
        public string Name { get; set; } = string.Empty;
        public string TaskText { get; set; } = string.Empty;
        public string CreatorName { get; set; } = string.Empty;
        [Required]
        public string Comment { get; set; } = string.Empty;
        public bool IsSubmitted { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreateDate { get; set; }
        public List<FilePathEntity> Files { get; set; } = [];
        public List<FilePathEntity> StudentFiles { get; set; } = [];
        public List<CommentEntity> Comments { get; set; } = [];
    }
}
