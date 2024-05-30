using LearningPlatform.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearningPlatform.ViewModels
{
    public class StudentTaskViewModel
    {
        [HiddenInput]
        public int TaskId { get; set; }
        public List<StudentTaskEntity> StudentTasks { get; set; } = [];
    }
}
