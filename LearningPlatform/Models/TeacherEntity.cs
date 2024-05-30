namespace LearningPlatform.Models
{
    public class TeacherEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public UserEntity? User { get; set; }
        public List<LessonEntity> Lessons { get; set; } = [];
        public List<CourseEntity> Courses { get; set; } = [];
        public List<GradeEntity> Grades { get; set; } = [];
        public List<ClassEntity> Classes { get; set; } = [];
        public List<GroupEntity> Groups { get; set; } = [];
    }
}