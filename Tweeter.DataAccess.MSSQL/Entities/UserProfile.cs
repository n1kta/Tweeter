using System.Collections.Generic;

namespace Tweeter.DataAccess.MSSQL.Entities
{
    public class UserProfile : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string BIO { get; set; }

        public string Photo { get; set; }

        public short Phone { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public ICollection<Tweet> Tweets { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<TweetLike> TweetLikes { get; set; }

        public ICollection<CommentLike> CommentLikes { get; set; }

        /// <summary>
        /// Person who following (From)
        /// </summary>
        public ICollection<Follower> Followings { get; set; }

        /// <summary>
        /// Person subscribed to (To)
        /// </summary>
        public ICollection<Follower> Followers { get; set; }
    }
}
