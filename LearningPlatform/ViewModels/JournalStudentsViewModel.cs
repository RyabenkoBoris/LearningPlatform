using LearningPlatform.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatform.ViewModels
{
    public class JournalStudentsViewModel
    {
        [HiddenInput]
        public int JournalId { get; set; }
        public List<UserEntity> Students { get; set; } = [];
        public string GroupName { get; set; } = string.Empty;
    }
}
