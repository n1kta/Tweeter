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
    }
}
