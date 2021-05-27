using Tweeter.Domain.Dtos;

namespace Tweeter.Domain.Contracts
{
    public interface IJwtService
    {
        public string GenerateToken(BaseAuthDto dto);

        public string DecodeToken(string token);
    }
}
