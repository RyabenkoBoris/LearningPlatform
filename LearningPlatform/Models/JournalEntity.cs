namespace LearningPlatform.Models
{
    public class JournalEntity
    {
        public int Id { get; set; }
        public int LessonId { get; set; }
        public LessonEntity? Lesson { get; set; }
        public byte Semester { get; set; }
        public bool Visible { get; set; }
        public int GroupId { get; set; }
        public GroupEntity? Group { get; set; }
        public List<GradeEntity> Grades { get; set; } = [];
    }
}
