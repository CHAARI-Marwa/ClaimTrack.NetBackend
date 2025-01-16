
using ClaimTrack.NetBackend.Context;
using ClaimTrack.NetBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace ClaimTrack.NetBackend.Repositories
{
    public class ReclamationRepository : IReclamationRepository
    {
        private readonly AppDbContext _context;

        public ReclamationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Reclamation> GetByIdAsync(int id)
        {
            return await _context.Set<Reclamation>()
                                 .Include(r => r.User)
                                 .Include(r => r.Article)
                                 //.Include(r => r.Intervention)
                                 .FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task AddAsync(Reclamation reclamation)
        {
            await _context.Set<Reclamation>().AddAsync(reclamation);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Reclamation>> GetAllAsync()
        {
            return await _context.Reclamations
                                 .Include(r => r.User)
                                 .Include(r => r.Article)
                                 .ToListAsync();
        }
        public async Task UpdateAsync(Reclamation reclamation)
        {
            var existingReclamation = await _context.Reclamations.FindAsync(reclamation.Id);
            if (existingReclamation != null)
            {
                _context.Entry(existingReclamation).CurrentValues.SetValues(reclamation);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Reclamation with ID {reclamation.Id} not found.");
            }
        }
        public async Task DeleteAsync(int id)
        {
            var reclamation = await _context.Reclamations.FindAsync(id);
            if (reclamation != null)
            {
                _context.Reclamations.Remove(reclamation);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Reclamation with ID {id} not found.");
            }
        }
        public async Task<Reclamation> GetByArticleIdAsync(int articleId)
        {
            return await _context.Reclamations
                .FirstOrDefaultAsync(r => r.IdArticle == articleId);
        }

        // Supprimer une réclamation par l'id de l'article
        public async Task DeleteByArticleIdAsync(int articleId)
        {
            var reclamation = await GetByArticleIdAsync(articleId);
            if (reclamation == null)
            {
                throw new KeyNotFoundException($"No reclamation found for article ID {articleId}.");
            }

            _context.Reclamations.Remove(reclamation);
            await _context.SaveChangesAsync();
        }
    }
}