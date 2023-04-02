using baseProject.Api.Dtos;
using baseProject.Domain.Models;

namespace baseProject.Domain.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<bool> RegisterAsync(UserDto userDto);
        public Task<String> LoginAsync(UserDto userDto);
    }
}
