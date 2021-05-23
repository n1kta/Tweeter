namespace Tweeter.DataAccess.MSSQL.Entities
{
    public class Follower
    {
        public int FromUserId { get; set; }

        public UserProfile FromUser { get; set; }

        public int ToUserId { get; set; }

        public UserProfile ToUser { get; set; }
    }
}
