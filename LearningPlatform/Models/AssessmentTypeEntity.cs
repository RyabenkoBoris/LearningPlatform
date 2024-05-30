namespace LearningPlatform.Models
{
    public class AssessmentTypeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<GradeEntity> Grades { get; set; } = [];

    }
}
