using System.Threading.Tasks;
using core.domain.Interfaces.Repositories;
using core.domain.Interfaces;
using core.domain.Entities;


namespace application.app.Services
{
    public class UserService : InterfaceUserRepository
    {
        private readonly InterfaceUserRepository _userRepository;

        public UserService(InterfaceUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await _userRepository.UserExistsAsync(username);
        }

        public async Task CreateUserAsync(User user)
        {
            await _userRepository.CreateUserAsync(user);
        }
    }
}

