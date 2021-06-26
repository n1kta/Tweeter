namespace Tweeter.Domain.Dtos
{
    public class CreatCommentDto
    {
        public string Description { get; set; }

        public int UserProfileId { get; set; }
        
        public int TweetId { get; set; }
    }
}