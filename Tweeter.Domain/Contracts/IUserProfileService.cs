using Tweeter.Domain.Dtos;

namespace Tweeter.Domain.Contracts
{
    public interface IUserProfileService
    {
        UserProfileDto GetCurrentUserProfile();
    }
}
