namespace LearningPlatform.Models
{
    public class SpecialityEntity
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<GroupEntity> Groups { get; set; } = [];
    }
}
