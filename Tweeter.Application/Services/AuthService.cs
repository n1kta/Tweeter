using Tweeter.DataAccess.MSSQL.Entities;
using Tweeter.Domain.Contracts;
using Tweeter.Domain.Dtos;
using System;
using Tweeter.Application.ExceptionMessage;
using Tweeter.Application.Helpers;
using Tweeter.Domain.HelperModels;

namespace Tweeter.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IJwtService _jwtService;
        private readonly IUserProfileService _userProfileService;

        public AuthService(IBaseRepository baseRepository,
                           IJwtService jwtService,
                           IUserProfileService userProfileService)
        {
            _baseRepository = baseRepository;
            _jwtService = jwtService;
            _userProfileService = userProfileService;
        }

        public ResultHelperModel Registration(RegistrationDto dto)
        {
            var isExist = IsUserExist(dto.UserName);

            var result = new ResultHelperModel
            {
                IsSuccess = false,
                ErrorMessage = null
            };

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
                _baseRepository.Create(newUser);
                _userProfileService.Create(newUser.Id, dto.UserProfile);

                result.IsSuccess = true;

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
                Token = _jwtService.GenerateToken(dto)
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
