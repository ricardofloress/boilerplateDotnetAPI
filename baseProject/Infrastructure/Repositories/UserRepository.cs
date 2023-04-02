using baseProject.Domain.Models;
using baseProject.Infrastructure.Context;
using baseProject.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace baseProject.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}
