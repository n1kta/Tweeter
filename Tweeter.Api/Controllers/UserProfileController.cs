using Microsoft.AspNetCore.Mvc;
using Tweeter.Domain.Contracts;
using Tweeter.Domain.Dtos;

namespace Tweeter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfile;

        public UserProfileController(IUserProfileService userProfile)
        {
            _userProfile = userProfile;
        }

        [HttpPut("{userId}")]
        public IActionResult Update(int userId, [FromBody]UserProfileDto dto)
        {
            var result = _userProfile.Update(userId, dto);
            
            return Ok(result);
        }

        [HttpPost]
        [Route("follow")]
        public IActionResult Follow([FromBody]FollowDto dto)
        {
            var result = _userProfile.ToggleFollow(dto);

            return Ok(result);
        }
    }
}