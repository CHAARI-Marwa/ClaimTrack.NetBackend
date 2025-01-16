
using ClaimTrack.NetBackend.Models;

namespace ClaimTrack.NetBackend.Repositories
{
    public interface IReclamationRepository
    {
        Task<Reclamation> GetByIdAsync(int id);
        Task AddAsync(Reclamation reclamation);
        Task<IEnumerable<Reclamation>> GetAllAsync();
        Task UpdateAsync(Reclamation reclamation);
        Task DeleteAsync(int id);
        Task<Reclamation> GetByArticleIdAsync(int articleId);
        Task DeleteByArticleIdAsync(int articleId);
    }
}
