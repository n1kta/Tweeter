using Tweeter.Domain.Dtos;
using Tweeter.Domain.HelperModels;

namespace Tweeter.Domain.Contracts
{
    public interface IUserProfileService
    {
        ResultHelperModel Create(int userId, UserProfileDto dto);

        ResultHelperModel Update(int userId, UserProfileDto dto);

        ResultHelperModel ToggleFollow(FollowDto dto);

        FollowerDto GetFollowerFollowing(int userProfileId);
    }
}
