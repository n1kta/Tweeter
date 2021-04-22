namespace Tweeter.DataAccess.MSSQL.Entities
{
    public sealed class User
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public byte[] Password { get; set; }

        public byte[] PasswordSalt { get; set; }
    }
}
