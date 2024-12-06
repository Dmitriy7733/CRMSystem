using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;


namespace CRMSystem.Models
{
    public class User : IdentityUser
    {

        [Required(ErrorMessage = "Поле Name обязательно для заполнения.")]
        public string Name { get; set; }
        public bool IsAdmin { get; set; }

    }
}
