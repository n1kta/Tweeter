namespace Tweeter.Domain.HelperModels
{
    public class PasswordHelperModel
    {
        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }
    }
}
