using Tweeter.DataAccess.MSSQL.Entities;
using Tweeter.Domain.Contracts;
using Tweeter.Domain.Dtos;
using System;
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
            var isExist = IsUserExist(dto.Email);

            if (isExist)
            {
                throw new Exception("User already exist. Pleas pick another Email or Username.");
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
                    UserName = newUser.UserName,
                    Email = newUser.Email
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
            var user = _baseRepository.Get<User>(x => x.Email == dto.Email);

            if (user == null)
            {
                throw new UnauthorizedAccessException(
                    "Invalid Username or Password. Please try again with another Username or Password.");
            }

            var computedHash = PasswordHelper.DecodePassword(user.PasswordSalt, dto.Password);

            for (var i = 0; i < computedHash.Length - 1; i++)
            {
                if (computedHash[i] != user.Password[i])
                {
                    throw new UnauthorizedAccessException(
                        "Invalid Username or Password. Please try again with another Username or Password.");
                }
            }

            // TODO: return token

            var result = new UserDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = ""
            };

            return result;
        }

        public void Logout()
        {
            throw new System.NotImplementedException();
        }

        private bool IsUserExist(string email)
        {
            var user = _baseRepository.Get<User>(x => x.Email == email);

            return user != null;
        }
    }
}
