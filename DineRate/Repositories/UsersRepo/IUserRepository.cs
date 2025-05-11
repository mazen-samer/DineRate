using DineRate.Models;

namespace DineRate.Repositories.UserRepo
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
        Task<bool> RegisterAsync(User user);
        Task<User?> AuthenticateAsync(string username, string password);
    }
}
