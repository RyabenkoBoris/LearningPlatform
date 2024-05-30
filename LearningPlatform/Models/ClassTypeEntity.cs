namespace LearningPlatform.Models
{
    public class ClassTypeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<ClassEntity> Classes { get; set; } = [];
    }
}
