using baseProject.Api.Dtos;
using baseProject.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace baseProject.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> LoginAsync(UserDto userDto)
        {
            string token = await _authService.LoginAsync(userDto);

            return Ok(token);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<bool>> RegisterAsync(UserDto userDto)
        {
            bool opResult = await _authService.RegisterAsync(userDto);

            return Ok(opResult);
        }

    }
}
