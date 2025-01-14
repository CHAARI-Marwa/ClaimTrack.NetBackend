using ClaimTrack.NetBackend.Models;

namespace ClaimTrack.NetBackend.Repositories
{
    public interface IUserRepository
    {
        Task<User> RegisterAsync(User user);
        Task<User?> LoginAsync(string email, string password);

    }
}
