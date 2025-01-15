using ClaimTrack.NetBackend.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClaimTrack.NetBackend.Models
{
    public class Reclamation
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Sujet { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime DateReclamation { get; set; }

        [Required]
        public int IdUser { get; set; }
        [ForeignKey("IdUser")]
        public virtual User User { get; set; }

        [Required]
        public int IdArticle { get; set; }
        [ForeignKey("IdArticle")]
        public virtual ArticleVendu Article { get; set; }

        public int? IdIntervention { get; set; }
        [ForeignKey("IdIntervention")]
        public virtual Intervention? Intervention { get; set; }

        [Required]
        public string Statut { get; set; }
    }
}