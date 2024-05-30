namespace LearningPlatform.Models
{
    public class TaskEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public short MaxMark { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime DueDate { get; set; }
        public string TaskText { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public UserEntity? User { get; set; }
        public int SectionId { get; set; }
        public SectionEntity? Section { get; set; }
        public List<StudentTaskEntity> StudentTask { get; set; } = [];
        public List<FilePathEntity> FilePaths { get; set; } = [];

    }
}