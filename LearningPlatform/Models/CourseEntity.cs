namespace LearningPlatform.Models
{
    public class CourseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public UserEntity? User { get; set; }
        public List<TeacherEntity> Teachers { get; set; } = [];
        public List<GroupEntity> Groups { get; set; } = [];
        public List<SectionEntity> Sections { get; set; } = [];
    }
}