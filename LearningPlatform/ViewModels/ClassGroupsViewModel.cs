using LearningPlatform.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.ViewModels
{
    public class ClassGroupsViewModel
    {
        [HiddenInput]
        public Guid TeacherId { get; set; }
        [HiddenInput]
        public int ClassId { get; set; }

        [Required(ErrorMessage = "Виберіть групу")]
        public int? GroupId { get; set; }
        public List<GroupEntity> Groups { get; set; } = [];
    }
}