using LearningPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.ViewModels
{
    public class MemberViewModel
    {
        public string OwnerName { get; set; } = string.Empty;
        [HiddenInput]
        public int CourseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<GroupEntity> Groups { get; set; } = [];
        [Required(ErrorMessage = "Виберіть викладача")]
        public Guid? TeacherId { get; set; }
        public List<TeacherEntity> Teachers { get; set; } = [];
        public List<UserEntity> Users { get; set; } = [];
    }
}
