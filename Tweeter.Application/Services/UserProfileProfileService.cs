using Microsoft.AspNetCore.Http;
using Tweeter.Domain.Contracts;
using Tweeter.Domain.Dtos;

namespace Tweeter.Application.Services
{
    public class UserProfileProfileService : IUserProfileService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserProfileProfileService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public UserProfileDto GetCurrentUserProfile()
        {
            var token = _httpContextAccessor.HttpContext.User.Identity.Name;
            return null;
        }
    }
}
