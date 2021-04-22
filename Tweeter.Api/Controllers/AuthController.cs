using Microsoft.AspNetCore.Mvc;

namespace Tweeter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        public IActionResult Register()
        {
            return Ok();
        }

        public IActionResult Login()
        {
            return Ok();
        }

        public IActionResult Logout()
        {
            return Ok();
        }
    }
}
