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
        public async Task<IEnumerable<ArticleVendu>> GetArticlesByUserIdAsync(int userId)
        {
            return await _context.ArticlesVendus
                                 .Where(article => article.IdUser == userId)
                                 .ToListAsync();
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
        public async Task DeleteArticleAsync(int id)
        {
            var article = await _context.ArticlesVendus.FindAsync(id);
            if (article != null)
            {
                _context.ArticlesVendus.Remove(article);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<ArticleVendu> UpdateArticleAsync(int id, ArticleVendu article)
        {
            var existingArticle = await _context.ArticlesVendus.FindAsync(id);

            if (existingArticle == null)
            {
                throw new ArgumentNullException(nameof(existingArticle), "L'article n'existe pas.");
            }

            existingArticle.NomArticle = article.NomArticle;
            existingArticle.IdUser = article.IdUser;
            existingArticle.DateAchat = article.DateAchat;
            existingArticle.DureeGarantie = article.DureeGarantie;

            _context.ArticlesVendus.Update(existingArticle);
            await _context.SaveChangesAsync();

            return existingArticle;
        }

    }
}
