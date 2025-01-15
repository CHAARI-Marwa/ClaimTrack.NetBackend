using System.ComponentModel.DataAnnotations;

namespace ClaimTrack.NetBackend.Models
{
    public class Intervention
    {
        [Key]
        public int Id { get; set; }  // Identifiant de l'intervention

        [Required]
        public int ReclamationId { get; set; }  // Clé étrangère vers la réclamation
        public virtual Reclamation Reclamation { get; set; }  // Navigation vers la réclamation


        [Required]
        public string Technicien { get; set; }  // Nom ou ID du technicien qui effectue l'intervention

        [Required]
        public int Duree { get; set; }  // Durée de l'intervention

        [Required]
        public int PieceRechangeId { get; set; }  // Clé étrangère vers la pièce de rechange utilisée
        public virtual PieceDetail PieceRechange { get; set; }  // Navigation vers la pièce de rechange

        public DateTime DateIntervention { get; set; } = DateTime.Now;  // Date de l'intervention
    }
}
