using Tweeter.Domain.Dtos;

namespace Tweeter.Domain.Contracts
{
    public interface IUserService
    {
        UserDto GetCurrentUser();

        ViewProfileDto GetViewProfileByUserName(string userName);
    }
}
