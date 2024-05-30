namespace LearningPlatform.Models
{
    public class FacultyEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Abbreviation { get; set; } = string.Empty;
        public List<DepartmentEntity> Departments { get; set; } = [];
    }
}
