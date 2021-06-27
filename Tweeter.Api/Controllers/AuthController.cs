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
            if (!ModelState.IsValid) return BadRequest();
            
            var result = _authService.Registration(dto);

            return Ok(result);

        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]LoginDto dto)
        {
            if (!ModelState.IsValid) return BadRequest();
            
            var result = _authService.Login(dto);

            return Ok(result);
        }

        public IActionResult Logout()
        {
            return Ok();
        }
    }
}
