using ClaimTrack.NetBackend.Context;
using ClaimTrack.NetBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace ClaimTrack.NetBackend.Repositories
{
    public class ArticleVenduRepository : IArticleVenduRepository
    {
        private readonly AppDbContext _context;

        public ArticleVenduRepository(AppDbContext context)
        {
            _context = context;
        }

        // POST : Ajouter un nouvel article
        public async Task<ArticleVendu> AddArticleAsync(ArticleVendu article)
        {
            await _context.ArticlesVendus.AddAsync(article);
            await _context.SaveChangesAsync();
            return article;  // Retourne l'article ajouté
        }

        // GET : Obtenir tous les articles
        public async Task<IEnumerable<ArticleVendu>> GetArticlesAsync()
        {
            return await _context.ArticlesVendus
                                 .Include(a => a.User)  // Inclut l'utilisateur lié
                                 .ToListAsync();  // Récupère tous les articles
        }

        // GET : Obtenir un article par son Id
        public async Task<ArticleVendu?> GetArticleByIdAsync(int id)
        {
            return await _context.ArticlesVendus
                                 .Include(a => a.User)  // Inclut l'utilisateur lié
                                 .FirstOrDefaultAsync(a => a.Id == id);  // Recherche par Id
        }
    }
}
