using LearningPlatform.Enum;

namespace LearningPlatform.Models
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public Role Role { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? DepartmentId { get; set; }
        public DepartmentEntity? Department { get; set; }
        public Guid? TeacherId { get; set; }
        public TeacherEntity? Teacher { get; set; }
        public int? GroupId { get; set; }
        public GroupEntity? Group { get; set; }
        public List<GradeEntity> Grades { get; set; } = [];
        public List<StudentTaskEntity> StudentTasks { get; set; } = [];
        public List<CourseEntity> Courses { get; set; } = [];
        public List<TaskEntity> Tasks { get; set; } = [];
        public List<CommentEntity> Comments { get; set; } = [];
    }
}