namespace LearningPlatform.Models
{
    public class GroupEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DepartmentId { get; set; }
        public DepartmentEntity? Department { get; set; }
        public int SpecialityId { get; set; }
        public SpecialityEntity? Speciality { get; set; }
        public List<UserEntity> Users { get; set; } = [];
        public List<CourseEntity> Courses { get; set; } = [];
        public List<ClassEntity> Classes { get; set; } = [];
        public List<TeacherEntity> Teachers { get; set; } = [];
        public List<JournalEntity> Journals { get; set; } = [];
    }
}
