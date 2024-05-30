namespace LearningPlatform.Models
{
    public class StudentTaskEntity
    {
        public int Id { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsSubmitted { get; set; }
        public short Mark { get; set; }
        public Guid UserId { get; set; }
        public UserEntity? User { get; set; }
        public int TaskId { get; set; }
        public TaskEntity? Task { get; set; }
        public List<FilePathEntity> FilePaths { get; set; } = [];
        public List<CommentEntity> Comments { get; set; } = [];
    }
}
