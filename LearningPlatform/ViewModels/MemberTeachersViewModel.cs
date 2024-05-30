using LearningPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.ViewModels
{
    public class MemberTeachersViewModel
    {
        public string TeacherName { get; set; } = string.Empty;
        [HiddenInput]
        public int CourseId { get; set; }
        [HiddenInput]
        [Required(ErrorMessage = "Виберіть викладача")]
        public Guid? TeacherId { get; set; }
        public List<UserEntity> Users { get; set; } = [];
    }
}
