using System.ComponentModel.DataAnnotations;

namespace Tweeter.Domain.Dtos
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(24, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
