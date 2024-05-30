namespace LearningPlatform.Models
{
    public class CommentEntity
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public int StudentTaskId { get; set; }
        public StudentTaskEntity? StudentTask { get; set; }
        public Guid UserId { get; set; }
        public UserEntity? User { get; set; }
    }
}
