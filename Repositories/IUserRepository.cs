using ClaimTrack.NetBackend.Models;

namespace ClaimTrack.NetBackend.Repositories
{
    public interface IUserRepository
    {
        Task<User> RegisterAsync(User user);
        Task<User?> LoginAsync(string email, string password);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<User> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);

    }
}
