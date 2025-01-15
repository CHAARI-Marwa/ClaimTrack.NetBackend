using ClaimTrack.NetBackend.Models;

namespace ClaimTrack.NetBackend.Repositories
{
    public interface IArticleVenduRepository
    {
        Task<ArticleVendu> AddArticleAsync(ArticleVendu article);
        Task<ArticleVendu?> GetArticleByIdAsync(int id);
        Task<IEnumerable<ArticleVendu>> GetArticlesAsync();
    }
}
