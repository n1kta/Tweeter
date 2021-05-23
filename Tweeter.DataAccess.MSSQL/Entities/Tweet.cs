using System.Collections.Generic;

namespace Tweeter.DataAccess.MSSQL.Entities
{
    public class Tweet : BaseEntity
    {
        public string Description { get; set; }

        public string Photo { get; set; }

        public UserProfile UserProfile { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<TweetLike> TweetLikes { get; set; }
    }
}
