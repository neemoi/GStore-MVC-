using System.ComponentModel.DataAnnotations;

namespace GStore.Models.ViewModels
{
    public class EditUserViewModel
    {
        public string? Email { get; set; }

        public string? Password { get; set; } 

        public string? ConfirmPassword { get; set; } 

        public string? Name { get; set; } 

        public string? Phone { get; set; }

        public string? Country { get; set; } 

        public string? City { get; set; } 

        public string? Address { get; set; }
    }
}
