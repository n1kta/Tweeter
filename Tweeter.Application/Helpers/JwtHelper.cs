using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.IdentityModel.Tokens;

namespace Tweeter.Application.Helpers
{
    public static class JwtHelper
    {
        private const string TOKEN_ERROR_EXCEPTION = "Token Error";
        private const string INCORRECT_TOKEN = "Incorrect token";

        public static string DecodeToken(string token)
        {
            token = token.Split(' ')[1];

            if (token == null) throw new Exception(TOKEN_ERROR_EXCEPTION);

            var tokenHandler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = AuthOptionsHelper.GetSymmetricSecurityKey(),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            tokenHandler.ValidateToken(token, validations, out var validatedToken);

            var securityToken = (JwtSecurityToken) validatedToken;

            var result = securityToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.NameId)?.Value;

            if (result == null) throw new Exception(INCORRECT_TOKEN);

            return result;
        }
    }
}
