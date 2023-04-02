using baseProject.Api.Dtos;
using baseProject.Domain.Models;
using baseProject.Domain.Services.Interfaces;
using baseProject.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace baseProject.Application.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<User> users = _userService.GetAllUsers().ToList();
            return Ok();
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult Get(int id)
        {
            User user = _userService.GetUserById(id);
            return Ok(user);
        }

        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            bool opResult = await _userService.DeleteUserAsync(id);

            return Ok(opResult);
        }

        [HttpPatch]
        [Route("UpdateUser")]
        public async Task<IActionResult> UpdateUserAsync(UserDto userDto)
        {
            bool opResult = await _userService.UpdateUserAsync(userDto);

            return Ok(opResult);
        }

    }
}