using Tweeter.Domain.Dtos;
using Tweeter.Domain.HelperModels;

namespace Tweeter.Domain.Contracts
{
    public interface IAuthService
    {
        ResultHelperModel Registration(RegistrationDto dto);

        UserDto Login(LoginDto dto);

        void Logout();
    }
}
