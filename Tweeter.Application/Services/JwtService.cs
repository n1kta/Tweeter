using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Tweeter.Application.Helpers;
using Tweeter.Domain.Contracts;
using Tweeter.Domain.Dtos;
using Tweeter.Domain.HelperModels;

namespace Tweeter.Application.Services
{
    public class JwtService : IJwtService
    {
        private const string TOKEN_ERROR_EXCEPTION = "Token Error";
        private const string INCORRECT_TOKEN = "Incorrect token";
        
        public string GenerateToken(BaseAuthDto dto)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, dto.UserName)
            };

            var cred = new SigningCredentials(AuthOptionsHelper.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = AuthOptionsHelper.ISSUER,
                Audience = AuthOptionsHelper.AUDIENCE,
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = cred
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        
        public string DecodeToken(string token)
        {
            token = token.Split(' ')[1];

            if (token == null) throw new ApiException(TOKEN_ERROR_EXCEPTION);

            var tokenHandler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = AuthOptionsHelper.GetSymmetricSecurityKey(),
                ValidateIssuer = false,
                ValidateAudience = false
            };

            // TODO: Exception
            tokenHandler.ValidateToken(token, validations, out var validatedToken);

            var securityToken = (JwtSecurityToken) validatedToken;

            var result = securityToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.NameId)?.Value;

            if (result == null) throw new ApiException(INCORRECT_TOKEN);

            return result;
        }
    }
}
