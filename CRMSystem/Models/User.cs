using System.ComponentModel.DataAnnotations;

namespace CRMSystem.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле Name обязательно для заполнения.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Пароль обязателен.")]
        [UIHint("password")]
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

    }
}
