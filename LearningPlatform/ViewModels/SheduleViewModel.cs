using LearningPlatform.Models;

namespace LearningPlatform.ViewModels
{
    public class SheduleViewModel
    {
        public List<ClassEntity> Classes { get; set; } = [];
        public List<ClassTypeEntity> ClassTypes { get; set; } = [];
        public List<string> SheduleSample { get; set; } = [];
    }
}
