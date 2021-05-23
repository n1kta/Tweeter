namespace Tweeter.DataAccess.MSSQL.Entities
{
    public class TweetLike
    {
        public int UserProfileId { get; set; }

        public UserProfile UserProfile { get; set; }

        public int TweetId { get; set; }

        public Tweet Tweet { get; set; }
    }
}
