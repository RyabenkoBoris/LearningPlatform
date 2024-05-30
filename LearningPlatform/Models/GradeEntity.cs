namespace LearningPlatform.Models
{
    public class GradeEntity
    {
        public int Id { get; set; }
        public int Mark { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public UserEntity? User { get; set; }
        public int JournalId { get; set; }
        public JournalEntity? Journal { get; set; }
        public Guid TeacherId { get; set; }
        public TeacherEntity? Teacher { get; set; }
        public int AssessmentTypeId { get; set; }
        public AssessmentTypeEntity? AssessmentType { get; set; }
    }
}
