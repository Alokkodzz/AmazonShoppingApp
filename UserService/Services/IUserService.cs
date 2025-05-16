using UserService.Models;

namespace UserService.Services
{
    public interface IUserService
    {
        Task<User?> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(User user);
    }
}
