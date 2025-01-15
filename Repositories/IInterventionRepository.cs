using ClaimTrack.NetBackend.Models;

namespace ClaimTrack.NetBackend.Repositories
{
    public interface IInterventionRepository
    {
        Task<IEnumerable<Intervention>> GetAllInterventionsAsync();
        Task<Intervention> GetInterventionByIdAsync(int id);
        Task CreateInterventionAsync(Intervention intervention);
        Task UpdateInterventionAsync(Intervention intervention);
        Task DeleteInterventionAsync(int id);
    }
}
