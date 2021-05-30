using Tweeter.Domain.Dtos;
using Tweeter.Domain.HelperModels;

namespace Tweeter.Domain.Contracts
{
    public interface ITweetService
    {
        ResultHelperModel Create(int userId, TweetDto dto);
    }
}