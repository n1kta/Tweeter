using Tweeter.Domain.Dtos;

namespace Tweeter.Domain.Contracts
{
    public interface IAuthService
    {
        UserDto Registration(RegistrationDto dto);

        UserDto Login(LoginDto dto);

        void Logout();
    }
}
