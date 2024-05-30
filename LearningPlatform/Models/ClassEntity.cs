namespace LearningPlatform.Models
{
    public class ClassEntity
    {
        public int Id { get; set; }
        public short LessonNumber { get; set; }
        public bool isFirstWeek { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public int LessonId { get; set; }
        public LessonEntity? Lesson { get; set; }
        public Guid TeacherId { get; set; }
        public TeacherEntity? Teacher { get; set; }
        public int SheduleSampleId { get; set; }
        public SheduleSampleEntity? SheduleSample { get; set; }
        public int ClassTypeId { get; set; }
        public ClassTypeEntity? ClassType { get; set; }
        public List<GroupEntity> Groups { get; set; } = [];
    }
}