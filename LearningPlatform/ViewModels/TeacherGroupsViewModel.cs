using LearningPlatform.Models;
using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.ViewModels
{
    public class TeacherGroupsViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Виберіть групу")]
        public int? GroupId { get; set; }
        public List<GroupEntity> Groups { get; set; } = [];
    }
}
