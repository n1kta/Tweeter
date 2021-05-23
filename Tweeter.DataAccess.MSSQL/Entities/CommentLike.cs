namespace Tweeter.DataAccess.MSSQL.Entities
{
    public class CommentLike
    {
        public int UserProfileId { get; set; }

        public UserProfile UserProfile { get; set; }

        public int CommentId { get; set; }

        public Comment Comment { get; set; }
    }
}
