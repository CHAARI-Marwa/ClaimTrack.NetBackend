namespace ClaimTrack.NetBackend.Dto
{
    public class InterventionDTO
    {
        public int Id { get; set; }
        public int ReclamationId { get; set; }
        public string Technicien { get; set; }
        public int Duree { get; set; }
        public int PieceRechangeId { get; set; }
        public DateTime DateIntervention { get; set; }
    }
}
