using Microsoft.AspNetCore.Http;
using Tweeter.Application.Helpers;
using Tweeter.DataAccess.MSSQL.Entities;
using Tweeter.Domain.Contracts;
using Tweeter.Domain.Dtos;

namespace Tweeter.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBaseRepository _baseRepository;

        private const string AUTHORIZATION_HEADER = "Authorization";

        public UserService(IHttpContextAccessor httpContextAccessor,
                           IBaseRepository baseRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _baseRepository = baseRepository;
        }

        public UserInfoDto GetCurrentUser()
        {
            var token = _httpContextAccessor.HttpContext.Request.Headers[AUTHORIZATION_HEADER];

            var userName = JwtHelper.DecodeToken(token);

            var user = _baseRepository.Get<User>(x => x.UserName == userName);

            var result = new UserInfoDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Photo = string.Empty
            };

            return result;
        }
    }
}
