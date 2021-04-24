using System;
using Moq;
using NUnit.Framework;
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

            var baseRepositoryMock = new Mock<IBaseRepository>();
            baseRepositoryMock
                .Setup(m => m.Get<User>(x => x.UserName == dto.UserName))
                .Returns(() => new User
                {
                    UserName = "n1kta"
                });

            var service = new AuthService(baseRepositoryMock.Object);

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
                    UserName = "n1kta",
                    Password = new byte[5],
                    PasswordSalt = new byte[5]
                });

            var service = new AuthService(baseRepositoryMock.Object);

            // act
            var result = service.Login(dto);

            // assert
            Assert.Throws<UnauthorizedAccessException>(() => Login_ShouldNotReturnToken(userName, password));
            Assert.Throws<Exception>(() => Login_ShouldNotReturnToken(userName, password));
        }
    }
}
