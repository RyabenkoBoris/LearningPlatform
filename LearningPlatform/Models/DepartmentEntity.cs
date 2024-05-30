namespace LearningPlatform.Models
{
    public class DepartmentEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Abbreviation { get; set; } = string.Empty;
        public int FacultyId { get; set; }
        public FacultyEntity? Faculty { get; set; }
        public List<GroupEntity> Groups { get; set; } = [];
        public List<UserEntity> Users { get; set; } = [];
        public List<LessonEntity> Lessons { get; set; } = [];
    }
}
