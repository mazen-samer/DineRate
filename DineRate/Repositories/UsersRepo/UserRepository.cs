using DineRate.Data;
using DineRate.Models;
using DineRate.Repositories.UserRepo;
using Microsoft.EntityFrameworkCore;

namespace DineRate.Repositories.UsersRepo
{
    public class UserRepository : IUserRepository
    {
        public AppDbContext context;

        public UserRepository(AppDbContext _context)
        {
            context = _context;
        }

        public async Task<bool> RegisterAsync(User user)
        {
            var exists = await context.Users.AnyAsync(u => u.Username == user.Username || u.Email == user.Email);
            if (exists)
                return false;
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
