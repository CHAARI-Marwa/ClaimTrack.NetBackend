using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClaimTrack.NetBackend.Models
{
    public class ArticleVendu
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom de l'article est requis.")]
        public string NomArticle { get; set; }

        [Required(ErrorMessage = "L'identifiant de l'utilisateur est requis.")]
        public int IdUser { get; set; }
        [ForeignKey("IdUser")]
        public User User { get; set; }

        [Required(ErrorMessage = "La date d'achat est requise.")]
        public DateTime DateAchat { get; set; }

        [Required(ErrorMessage = "La durée de garantie est requise.")]
        public int DureeGarantie { get; set; }
    }
}