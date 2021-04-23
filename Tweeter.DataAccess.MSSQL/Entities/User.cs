namespace Tweeter.DataAccess.MSSQL.Entities
{
    public sealed class User : BaseEntity
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public byte[] Password { get; set; }

        public byte[] PasswordSalt { get; set; }

        public UserProfile UserProfile { get; set; }
    }
}
