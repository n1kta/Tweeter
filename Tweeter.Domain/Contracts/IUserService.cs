using Tweeter.Domain.Dtos;

namespace Tweeter.Domain.Contracts
{
    public interface IUserService
    {
        UserInfoDto GetCurrentUser();
    }
}
