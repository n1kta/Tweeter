using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Tweeter.Application.Helpers
{
    public static class AuthOptionsHelper
    {
        public const string ISSUER = "Issuer";
        public const string AUDIENCE = "Audience";
        public const string KEY = "super security key";

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));

            return key;
        }
    }
}
