using Tweeter.DataAccess.MSSQL.Entities;
using Tweeter.Domain.Contracts;
using Tweeter.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Tweeter.Application.ExceptionMessage;
using Tweeter.Application.Helpers;

namespace Tweeter.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseRepository _baseRepository;

        public AuthService(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public UserDto Registration(RegistrationDto dto)
        {
            var isExist = IsUserExist(dto.UserName);

            if (isExist)
            {
                throw new Exception(AuthExceptionMessages.USER_ALREADY_EXIST);
            }

            var encodedPassword = PasswordHelper.EncodePassword(dto.Password);

            var newUser = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
                Password = encodedPassword.PasswordHash,
                PasswordSalt = encodedPassword.PasswordSalt,
                AddedDate = DateTime.Now
            };

            try
            {
                _baseRepository.Create<User>(newUser);
                _baseRepository.SaveChanges();

                var result = new UserDto
                {
                    Token = GenerateToken(dto)
                };

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString(), ex);
            }
        }

        public UserDto Login(LoginDto dto)
        {
            var user = _baseRepository.Get<User>(x => x.UserName == dto.UserName);

            if (user == null)
            {
                throw new UnauthorizedAccessException(
                    AuthExceptionMessages.INVALID_USERNAME_OR_PASSWORD);
            }

            var computedHash = PasswordHelper.DecodePassword(user.PasswordSalt, dto.Password);

            for (var i = 0; i < computedHash.Length - 1; i++)
            {
                if (computedHash[i] != user.Password[i])
                {
                    throw new UnauthorizedAccessException(
                        AuthExceptionMessages.INVALID_USERNAME_OR_PASSWORD);
                }
            }

            var result = new UserDto
            {
                Token = GenerateToken(dto)
            };

            return result;
        }

        public void Logout()
        {
            throw new System.NotImplementedException();
        }

        private string GenerateToken(BaseAuthDto dto)
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

        private bool IsUserExist(string userName)
        {
            var user = _baseRepository.Get<User>(x => x.UserName == userName);

            return user != null;
        }
    }
}
