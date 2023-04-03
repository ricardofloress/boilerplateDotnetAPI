using baseProject.Api.Dtos;

namespace baseProject.Domain.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<bool> RegisterAsync(UserDto userDto);
        public Task<String> LoginAsync(UserDto userDto);
    }
}
