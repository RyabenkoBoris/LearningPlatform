using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.ViewModels
{
    public class FileViewModel
    {
        [HiddenInput]
        public int StudentTaskId { get; set; }
        [HiddenInput]
        public int TaskId { get; set; }
        [MinLength(1, ErrorMessage = "Завантажте файли")]
        public List<IFormFile> Files { get; set; } = [];
    }
}
