using System.Collections.Generic;

namespace Tweeter.DataAccess.MSSQL.Entities
{
    public class Comment : BaseEntity
    {
        public string Description { get; set; }

        public UserProfile UserProfile { get; set; }

        public Tweet Tweet { get; set; }

        public ICollection<CommentLike> CommentLikes { get; set; }
    }
}
