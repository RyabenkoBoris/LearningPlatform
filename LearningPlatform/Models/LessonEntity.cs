namespace LearningPlatform.Models
{
    public class LessonEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<TeacherEntity> Teachers { get; set; } = [];
        public List<JournalEntity> Journals { get; set; } = [];
        public List<ClassEntity> Classes { get; set; } = [];
        public List<DepartmentEntity> Departaments { get; set; } = [];
    }
}
