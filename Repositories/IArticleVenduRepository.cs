using ClaimTrack.NetBackend.Models;

namespace ClaimTrack.NetBackend.Repositories
{
    public interface IArticleVenduRepository
    {
        Task<ArticleVendu> AddArticleAsync(ArticleVendu article);
        Task<ArticleVendu?> GetArticleByIdAsync(int id);
        Task<IEnumerable<ArticleVendu>> GetArticlesAsync();
        Task<IEnumerable<ArticleVendu>> GetArticlesByUserIdAsync(int id);
        Task DeleteArticleAsync(int id);
        Task<ArticleVendu> UpdateArticleAsync(int id, ArticleVendu article);
    }
}
