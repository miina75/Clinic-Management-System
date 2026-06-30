using System.ComponentModel.DataAnnotations;
namespace Clinic.Models
{
    public class Users
    {
        public int UserId { get; set; }

        public string Email { get; set; }

        [Required]
        public string Username { get; set; } = "";

        [Required]
        public string PasswordHash { get; set; } = "";

        [Required]
        public string Role { get; set; } = "";
    }
}