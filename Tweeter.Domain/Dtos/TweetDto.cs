using System.ComponentModel.DataAnnotations;

namespace Tweeter.Domain.Dtos
{
    public class TweetDto
    {
        [Required]
        public string Description { get; set; }

        public string Photo { get; set; }
    }
}