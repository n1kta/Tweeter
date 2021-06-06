using Microsoft.AspNetCore.Mvc;
using Tweeter.Domain.Contracts;
using Tweeter.Domain.Dtos;

namespace Tweeter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TweetController : ControllerBase
    {
        private readonly ITweetService _tweetService;
        
        public TweetController(ITweetService tweetService)
        {
            _tweetService = tweetService;
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
    }
}