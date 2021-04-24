using System.ComponentModel.DataAnnotations;

namespace Tweeter.Domain.Dtos
{
    public class LoginDto : BaseAuthDto
    {
        [Required]
        [StringLength(24, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
