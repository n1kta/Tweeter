using System.Security.Cryptography;
using System.Text;
using Tweeter.Domain.HelperModels;

namespace Tweeter.Application.Helpers
{
    public static class PasswordHelper
    {
        public static PasswordHelperModel EncodePassword(string password)
        {
            using var hmac = new HMACSHA512();

            var bytePassword = Encoding.UTF8.GetBytes(password);

            var hashPassword = hmac.ComputeHash(bytePassword);
            var saltPassword = hmac.Key;

            var result = new PasswordHelperModel
            {
                PasswordHash = hashPassword,
                PasswordSalt = saltPassword
            };

            return result;
        }

        public static byte[] DecodePassword(byte[] saltPassword, string password)
        {
            using var hmac = new HMACSHA512(saltPassword);

            var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return computeHash;
        }
    }
}
