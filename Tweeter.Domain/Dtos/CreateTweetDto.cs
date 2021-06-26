using System.ComponentModel.DataAnnotations;

namespace Tweeter.Domain.Dtos
{
    public class CreateTweetDto
    {
        [Required]
        public string Description { get; set; }

        public string Photo { get; set; }

        public bool IsEveryoneMode { get; set; }

        public UserProfileDto UserProfile { get; set; }
    }
}