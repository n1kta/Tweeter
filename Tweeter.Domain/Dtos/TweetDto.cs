namespace Tweeter.Domain.Dtos
{
    public class TweetDto
    {
        public string Description { get; set; }

        public string Photo { get; set; }

        public UserProfileDto UserProfile { get; set; }
    }
}