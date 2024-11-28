using CRMSystem.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRMSystem.ViewModels
{
    public class ManagerListViewModel
    {
        [Required(ErrorMessage = "Поле Name обязательно для заполнения.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Пароль обязателен.")]
        [DataType(DataType.Password)]
        [UIHint("password")]
        public string Password { get; set; }

        public IEnumerable<User> Users { get; set; } = new List<User>(); // Инициализация коллекции
    }
}