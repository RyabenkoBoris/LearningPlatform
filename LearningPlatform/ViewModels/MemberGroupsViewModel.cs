using LearningPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.ViewModels
{
    public class MemberGroupsViewModel
    {
        public string GroupName { get; set; } = string.Empty;
        [HiddenInput]
        public int CourseId { get; set; }

        [HiddenInput]
        [Required(ErrorMessage = "Виберіть групу")]
        public int? GroupId { get; set; }
        public List<GroupEntity> Groups { get; set; } = [];

    }
}
