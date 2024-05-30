using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.ViewModels
{
    public class SpecialityViewModel
    {
        public int Id { get; set; }
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Введіть додатне число")]
        [Range(1, 1000, ErrorMessage = "Діапазон спеціальності від 1 до 1000")]
        public int Code { get; set; }
        [Required(ErrorMessage = "Введіть назву спеціальності")]
        [MaxLength(100, ErrorMessage = "Максимальна довжина - 100")]
        public string Name { get; set; } = string.Empty;
    }
}
