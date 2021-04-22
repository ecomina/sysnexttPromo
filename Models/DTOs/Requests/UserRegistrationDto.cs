using System.ComponentModel.DataAnnotations;

namespace Authentic.Models.DTOs.UserRegistrationDto
{
    public class UserRegistrationDto
    {

        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email {get; set;}
        public string Password { get; set; }
    }
}