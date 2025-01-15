using ClaimTrack.NetBackend.Context;
using ClaimTrack.NetBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace ClaimTrack.NetBackend.Repositories
{
    public class InterventionRepository : IInterventionRepository
    {
        private readonly AppDbContext _context;

        public InterventionRepository(AppDbContext context)
        {
            _context = context;
        }

        // Méthode pour récupérer toutes les interventions
        public async Task<IEnumerable<Intervention>> GetAllInterventionsAsync()
        {
            return await _context.Interventions
                .Include(i => i.Reclamation)  // Inclure la réclamation associée
                .Include(i => i.PieceRechange) // Inclure la pièce de rechange
                .ToListAsync();
        }

        // Méthode pour récupérer une intervention par ID
        public async Task<Intervention> GetInterventionByIdAsync(int id)
        {
            return await _context.Interventions
                .Include(i => i.Reclamation)

                .Include(i => i.PieceRechange)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        // Méthode pour créer une nouvelle intervention
        public async Task CreateInterventionAsync(Intervention intervention)
        {
            _context.Interventions.Add(intervention);
            await _context.SaveChangesAsync();
        }

        // Méthode pour mettre à jour une intervention
        public async Task UpdateInterventionAsync(Intervention intervention)
        {
            _context.Interventions.Update(intervention);
            await _context.SaveChangesAsync();
        }

        // Méthode pour supprimer une intervention
        public async Task DeleteInterventionAsync(int id)
        {
            var intervention = await GetInterventionByIdAsync(id);
            if (intervention != null)
            {
                _context.Interventions.Remove(intervention);
                await _context.SaveChangesAsync();
            }
        }
    }
}
