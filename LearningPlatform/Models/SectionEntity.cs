namespace LearningPlatform.Models
{
    public class SectionEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CourseId { get; set; }
        public CourseEntity? Course { get; set; }
        public List<TaskEntity> Tasks { get; set; } = [];
    }
}