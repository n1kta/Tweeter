using Microsoft.AspNetCore.Mvc;
using Tweeter.Application.Enums;
using Tweeter.Application.Helpers;
using Tweeter.Domain.Contracts;
using Tweeter.Domain.Dtos;

namespace Tweeter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ILikeService _likeCommentService;
        private readonly ICommentService _commentService;
        
        public CommentController(ResolverHelper.LikeServiceResolver likeService,
                                ICommentService commentService)
        {
            _likeCommentService = likeService(ConcreteLikeServiceImplementationEnum.Comment);
            _commentService = commentService;
        }
        
        [HttpPost]
        [Route("addComment")]
        public IActionResult AddComment(CreatCommentDto dto)
        {
            var result = _commentService.AddComment(dto);

            return Ok(result);
        }

        [HttpPost]
        [Route("likeComment")]
        public IActionResult LikeComment([FromBody] LikeDto dto)
        {
            var result = _likeCommentService.ToggleLike(dto);

            return Ok(result);
        }
    }
}