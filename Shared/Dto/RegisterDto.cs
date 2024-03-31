using Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace Shared.Dto
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
        [Required]
        [StringLength(50)]
        public string FullName { get; set; }
      

    }
}
