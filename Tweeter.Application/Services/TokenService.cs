using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Tweeter.Application.Helpers;
using Tweeter.Domain.Contracts;
using Tweeter.Domain.Dtos;

namespace Tweeter.Application.Services
{
    public class TokenService : ITokenService
    {
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
    }
}
