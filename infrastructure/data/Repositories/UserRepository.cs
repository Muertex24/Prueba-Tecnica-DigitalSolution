using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using core.domain.Entities;
using core.domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace infrastructure.data.Repositories
{
    public class UserRepository : InterfaceUserRepository
    {
        private readonly SocialNetworkContext _context;

        public UserRepository(SocialNetworkContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FindAsync(username);
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }

        public async Task CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
