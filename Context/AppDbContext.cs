
using ClaimTrack.NetBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace ClaimTrack.NetBackend.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ArticleVendu> ArticlesVendus { get; set; }
        public DbSet<Reclamation> Reclamations { get; set; }
        public DbSet<Intervention> Interventions { get; set; }
        public DbSet<PieceDetail> PieceDetails { get; set; }
    }
}
