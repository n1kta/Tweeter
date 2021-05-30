using Microsoft.AspNetCore.Mvc;
using Tweeter.Domain.Contracts;
using Tweeter.Domain.Dtos;

namespace Tweeter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("currentUser")]
        public IActionResult GetCurrentUser()
        {
            var result = _userService.GetCurrentUser();
            return Ok(result);
        }

        [HttpGet]
        [Route("getViewProfile/{userName}")]
        public IActionResult GetViewProfileByUserName(string userName)
        {
            var result = _userService.GetViewProfileByUserName(userName);
            return Ok(result);
        }
    }
}
