using System.ComponentModel.DataAnnotations;

namespace ClaimTrack.NetBackend.Dto
{
    public class ArticleDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom de l'article est requis.")]
        public string NomArticle { get; set; }

        [Required(ErrorMessage = "L'identifiant de l'utilisateur est requis.")]
        public int IdUser { get; set; }

        [Required(ErrorMessage = "La date d'achat est requise.")]
        public DateTime DateAchat { get; set; }

        [Required(ErrorMessage = "La durée de garantie est requise.")]
        public int DureeGarantie { get; set; }
    }
}
