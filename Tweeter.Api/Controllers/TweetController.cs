using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using Tweeter.Application.Enums;
using Tweeter.Application.Helpers;
using Tweeter.Domain.Contracts;
using Tweeter.Domain.Dtos;

namespace Tweeter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TweetController : ControllerBase
    {
        private readonly ITweetService _tweetService;
        private readonly ILikeService _likeService;
        private readonly ILikeService _likeCommentService;
        private readonly ICommentService _commentService;
        
        public TweetController(ITweetService tweetService,
                                ResolverHelper.LikeServiceResolver likeService,
                                ICommentService commentService)
        {
            _tweetService = tweetService;
            _likeService = likeService(ConcreteLikeServiceImplementationEnum.Tweet);
            _likeCommentService = likeService(ConcreteLikeServiceImplementationEnum.Comment);
            _commentService = commentService;
        }
        
        [HttpPost("{userId}")]
        public IActionResult Create(int userId, [FromBody] TweetDto dto)
        {
            if (ModelState.IsValid)
            {
                var result = _tweetService.Create(userId, dto);
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet]
        [Route("getTweetsFollowers/{userProfileId}")]
        public IActionResult GetTweetsFollowers(int userProfileId)
        {
            var result = _tweetService.GetTweetsFollowers(userProfileId);

            return Ok(result);
        }

        [HttpPost]
        [Route("like")]
        public IActionResult Like([FromBody] LikeDto dto)
        {
            var result = _likeService.ToggleLike(dto);

            return Ok(result);
        }

        [HttpPost]
        [Route("addComment")]
        public IActionResult AddComment(CommentDto dto)
        {
            var result = _commentService.AddComment(dto);

            return Ok(result);
        }

        [HttpPost]
        [Route("likeComment")]
        public IActionResult LikeComment([FromBody] LikeDto dto)
        {
            var result = _likeService.ToggleLike(dto);

            return Ok(result);
        }
    }
}