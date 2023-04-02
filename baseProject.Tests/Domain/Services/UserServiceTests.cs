using baseProject.Domain.Models;
using baseProject.Domain.Services;
using baseProject.Domain.Services.Interfaces;
using baseProject.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace baseProject.Tests.Domain.Services
{
    public class UserServiceTests
    {

        [Fact(DisplayName = "Should return all users that have been mocked.")]
        public async Task UserService_GetAllUsers_ShouldReturnAllUsers()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            IUserService userService = new UserService(unitOfWorkMock.Object);

            unitOfWorkMock
               .Setup(mock => mock.Users.AllAsync())
               .ReturnsAsync(new List<User>()
               {
                   new User()
                   {
                       Id = 1,
                       Email = "admin.log@gmail.com",
                       PasswordHash = new byte[] { 1, 2, 3, 4 },
                       PasswordSalt = new byte[] { 1, 2, 3, 4 },
                       Role = Role.Admin.ToString()
                   },
                   new User()
                   {
                       Id = 2,
                       Email = "users.log@gmail.com",
                       PasswordHash = new byte[] { 1, 2, 3, 4 },
                       PasswordSalt = new byte[] { 1, 2, 3, 4 },
                       Role = Role.RegularUser.ToString()
                   }
               });

            // Act

            var allUsers = userService.GetAllUsers();

            //// Assert

            allUsers.Should().HaveCount(2);
        }
    }
}
