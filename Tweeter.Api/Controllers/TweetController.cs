using Microsoft.AspNetCore.Mvc;
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

        public TweetController(ITweetService tweetService,
                                ResolverHelper.LikeServiceResolver likeService)
        {
            _tweetService = tweetService;
            _likeService = likeService(ConcreteLikeServiceImplementationEnum.Tweet);
            
        }
        
        [HttpPost("{userId}")]
        public IActionResult Create(int userId, [FromBody] CreateTweetDto dto)
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
    }
}