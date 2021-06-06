using System;
using Moq;
using NUnit.Framework;
using Tweeter.Application.ExceptionMessage;
using Tweeter.Application.Helpers;
using Tweeter.Application.Services;
using Tweeter.DataAccess.MSSQL.Entities;
using Tweeter.Domain.Contracts;
using Tweeter.Domain.Dtos;

namespace Tweeter.Tests
{
    public class AuthServiceTest
    {
        [TestCase("n1kta", "qwerty12345")]
        public void Login_ShouldReturnToken(string userName, string password)
        {
            // arrange
            var dto = new LoginDto
            {
                UserName = userName,
                Password = password
            };

            var encodedPassword = PasswordHelper.EncodePassword(password);

            var baseRepositoryMock = new Mock<IBaseRepository>();
            baseRepositoryMock
                .Setup(m => m.Get<User>(x => x.UserName == dto.UserName))
                .Returns(() => new User
                {
                    UserName = userName,
                    Password = encodedPassword.PasswordHash,
                    PasswordSalt = encodedPassword.PasswordSalt
                });

            var jwtService = new JwtService();
            var userService = new UserService(null, null, null, null);
            var userProfileService = new UserProfileService(baseRepositoryMock.Object, null, userService);
            var service = new AuthService(baseRepositoryMock.Object, jwtService, userProfileService);

            // act
            var result = service.Login(dto);

            // assert
            Assert.IsNotEmpty(result.Token);
            Assert.IsNotNull(result.Token);
        }

        [TestCase("test1", "qwerty12345")]
        [TestCase("", "")]
        public void Login_ShouldNotReturnToken(string userName, string password)
        {
            // arrange
            var dto = new LoginDto
            {
                UserName = userName,
                Password = password
            };

            var baseRepositoryMock = new Mock<IBaseRepository>();
            baseRepositoryMock
                .Setup(m => m.Get<User>(x => x.UserName == dto.UserName))
                .Returns(() => new User
                {
                    UserName = userName,
                    Password = PasswordHelper.EncodePassword(password).PasswordHash,
                    PasswordSalt = PasswordHelper.EncodePassword(password).PasswordSalt
                });

            var jwtService = new JwtService();
            var userService = new UserService(null, null, null, null);
            var userProfileService = new UserProfileService(baseRepositoryMock.Object, null, userService);
            var service = new AuthService(baseRepositoryMock.Object, jwtService, userProfileService);

            // act

            // assert
            Assert.That(Assert.Throws<UnauthorizedAccessException>(() => service.Login(dto))?.Message == AuthExceptionMessages.INVALID_USERNAME_OR_PASSWORD);
        }
    }
}
