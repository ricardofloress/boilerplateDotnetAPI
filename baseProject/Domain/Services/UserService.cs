using baseProject.Api.Dtos;
using baseProject.Domain.Models;
using baseProject.Domain.Services.Interfaces;
using baseProject.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Security.Cryptography;

namespace baseProject.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _unitOfWork.Users.AllAsync().Result;
        }

        public User GetUserById(int id) => _unitOfWork.Users.GetByIdAsync(id).Result;

        public User GetUserByEmail(string email) => _unitOfWork.Users.GetByEmailAsync(email).Result;

        public async Task<bool> AddUserAsync(UserDto userDto)
        {
            CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User()
            {
                Email = userDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = userDto.Role,
            };

            bool operationResult = await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.CompleteAsync();

            return operationResult;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id); ;

            var userExists = user != null;

            if (!userExists) return false;

            bool operationResult = await _unitOfWork.Users.DeleteAsync(user);
            await _unitOfWork.CompleteAsync();

            return operationResult;
        }

        public async Task<bool> UpdateUserAsync(UserDto userDto)
        {
            var user = await _unitOfWork.Users.GetByEmailAsync(userDto.Email); ;

            var userExists = user != null;

            if (!userExists) return false;

            CreatePasswordHash(userDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.Email = userDto.Email;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Role = userDto.Role;

            bool operationResult = await _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.CompleteAsync();

            return operationResult;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

    }
}
