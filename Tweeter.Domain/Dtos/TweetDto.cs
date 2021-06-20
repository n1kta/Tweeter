using System.ComponentModel.DataAnnotations;

namespace Tweeter.Domain.Dtos
{
    public class TweetDto
    {
        public int Id { get; set; }
        
        [Required]
        public string Description { get; set; }

        public string Photo { get; set; }

        public string UserName { get; set; }

        public UserProfileDto UserProfile { get; set; }

        public int Likes { get; set; }

        public bool? IsLiked { get; set; }
    }
}