using Tweeter.DataAccess.MSSQL.Entities;
using Tweeter.Domain.Contracts;
using Tweeter.Domain.Dtos;
using System;
using Tweeter.Application.ExceptionMessage;
using Tweeter.Application.Helpers;

namespace Tweeter.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IBaseRepository baseRepository,
            ITokenService tokenService)
        {
            _baseRepository = baseRepository;
            _tokenService = tokenService;
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
                    Token = _tokenService.GenerateToken(dto)
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
                Token = _tokenService.GenerateToken(dto)
            };

            return result;
        }

        public void Logout()
        {
            throw new System.NotImplementedException();
        }

        private bool IsUserExist(string userName)
        {
            var user = _baseRepository.Get<User>(x => x.UserName == userName);

            return user != null;
        }
    }
}
