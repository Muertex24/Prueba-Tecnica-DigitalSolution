using core.domain.Entities;
using System.Threading.Tasks;

namespace core.domain.Interfaces.Repositories
{
    public interface InterfaceUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task<bool> UserExistsAsync(string username);
        Task CreateUserAsync(User user);
    }
}
