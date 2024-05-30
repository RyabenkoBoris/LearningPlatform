namespace LearningPlatform.Models
{
    public class FilePathEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public int? TaskId { get; set; }
        public TaskEntity? Task { get; set; }
        public int? StudentTaskId { get; set; }
        public StudentTaskEntity? StudentTask { get; set; }
    }
}