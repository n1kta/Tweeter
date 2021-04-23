using System;
using Microsoft.AspNetCore.Mvc;
using Tweeter.Domain.Contracts;
using Tweeter.Domain.Dtos;

namespace Tweeter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("registration")]
        public IActionResult Register([FromBody]RegistrationDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _authService.Registration(dto);

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    // TODO: Use ILogger
                    return BadRequest(ex.ToString());
                }
            }
            
            return BadRequest();
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]LoginDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = _authService.Login(dto);

                    return Ok(result);
                }
                catch (UnauthorizedAccessException ex)
                {
                    // TODO: Use ILogger
                    return Unauthorized(ex.ToString());
                }
                catch (Exception ex)
                {
                    // TODO: Use ILogger
                    return BadRequest(ex.ToString());
                }
            }
            return BadRequest();
        }

        public IActionResult Logout()
        {
            return Ok();
        }
    }
}
