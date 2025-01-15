using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClaimTrack.NetBackend.Models
{
    public class PieceDetail
    {
        [Key]
        public int PieceId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public String IntitulePiece { get; set; } = "";

        [Column(TypeName = "int")]
        public int Quantite { get; set; }
    }
}
