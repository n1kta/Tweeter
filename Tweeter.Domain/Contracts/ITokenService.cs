using Tweeter.Domain.Dtos;

namespace Tweeter.Domain.Contracts
{
    public interface ITokenService
    {
        string GenerateToken(BaseAuthDto dto);
    }
}
