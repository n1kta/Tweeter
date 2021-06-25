using Tweeter.Domain.Dtos;
using Tweeter.Domain.HelperModels;

namespace Tweeter.Domain.Contracts
{
    public interface ICommentService
    {
        ResultHelperModel AddComment(CommentDto dto);
    }
}