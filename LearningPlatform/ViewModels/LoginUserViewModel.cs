using System.ComponentModel.DataAnnotations;

namespace LearningPlatform.ViewModels
{
    public class LoginUserViewModel
    {
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Введіть Email")]
        public string Email { get; set; } = string.Empty;
        [DataType(DataType.Password, ErrorMessage = "Помилка")]
        [Required(ErrorMessage = "Введіть пароль"), MinLength(5, ErrorMessage = "Мінімальна довжина паролю 5")]
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; }
    }
}
