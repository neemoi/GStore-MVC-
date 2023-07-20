using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GStore.Models.ViewModels
{
    public class AddUserViewModel
    {
        [Required(ErrorMessage = "Email обязателен к заполнению")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Пароль обязателен к заполнению")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Поле не должно быть пустым")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; } = null!;

        public string? Name { get; set; } = null;

        public string? Phone { get; set; } = null;

        public string? Country { get; set; } = null;

        public string? City { get; set; } = null;

        public string? Address { get; set; } = null;
    }
}
