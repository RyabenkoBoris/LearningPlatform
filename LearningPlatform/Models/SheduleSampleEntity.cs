namespace LearningPlatform.Models
{
    public class SheduleSampleEntity
    {
        public int Id { get; set; }
        public string LessonTime1 { get; set; } = string.Empty;
        public string LessonTime2 { get; set; } = string.Empty;
        public string LessonTime3 { get; set; } = string.Empty;
        public string LessonTime4 { get; set; } = string.Empty;
        public string LessonTime5 { get; set; } = string.Empty;
        public string LessonTime6 { get; set; } = string.Empty;
        public List<ClassEntity> Classes { get; set; } = [];
    }
}
