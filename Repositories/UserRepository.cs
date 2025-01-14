using ClaimTrack.NetBackend.Context;
using ClaimTrack.NetBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace ClaimTrack.NetBackend.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> RegisterAsync(User user)
        {
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == user.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("A user with this email already exists.");
            }
            string hashedPassword = HashPassword(user.Password);
            user.Password = hashedPassword;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public string HashPassword(string plainTextPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(plainTextPassword);
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
            if (user != null && VerifyPassword(password, user.Password))
            {
                return user;
            }
            return null;
        }

        private bool VerifyPassword(string plainTextPassword, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(plainTextPassword, hashedPassword);
        }


    }

}
