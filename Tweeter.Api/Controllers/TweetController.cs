using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
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
        
        public TweetController(ITweetService tweetService,
                                ILikeService likeService)
        {
            _tweetService = tweetService;
            _likeService = likeService;
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
            var result = _tweetService.AddComment(dto);

            return Ok(result);
        }
    }
}