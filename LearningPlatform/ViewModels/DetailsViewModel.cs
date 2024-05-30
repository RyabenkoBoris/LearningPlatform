using LearningPlatform.Models;

namespace LearningPlatform.ViewModels
{
    public class DetailsViewModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public List<SectionEntity> Sections { get; set; } = [];

    }
}
