using Tweeter.Domain.Dtos;
using Tweeter.Domain.HelperModels;

namespace Tweeter.Domain.Contracts
{
    public interface ILikeService
    {
        ResultHelperModel ToggleLike(LikeDto dto);
    }
}