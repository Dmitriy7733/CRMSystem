using System.ComponentModel.DataAnnotations;

namespace CRMSystem.ViewModels
{
    public sealed class LoginViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]

        [UIHint("password")]
        public string Password { get; set; }
        //public string Role { get; set; } // Добавьте это поле
        public string ReturnUrl { get; set; } = "/";
        public bool RememberMe { get; set; }
    }
}