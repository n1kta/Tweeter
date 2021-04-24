using System.ComponentModel.DataAnnotations;

namespace Tweeter.Domain.Dtos
{
    public class BaseAuthDto
    {
        [Required]
        public string UserName { get; set; }
    }
}
