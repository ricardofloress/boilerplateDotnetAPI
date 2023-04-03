using baseProject.Api.Dtos;
using baseProject.Domain.Models;

namespace baseProject.Domain.Services.Interfaces
{
    public interface IUserService
    {
        public IEnumerable<User> GetAllUsers();
        public User GetUserById(int id);
        public User GetUserByEmail(string email);
        public Task<bool> AddUserAsync(UserDto userDto);
        public Task<bool> DeleteUserAsync(int id);
        public Task<bool> UpdateUserAsync(UserDto userDto);
    }
}
