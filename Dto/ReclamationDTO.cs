namespace ClaimTrack.NetBackend.Dto
{
    public class ReclamationDTO
    {
        public string Sujet { get; set; }
        public string Description { get; set; }
        public DateTime DateReclamation { get; set; }
        public int IdUser { get; set; }
        public int IdArticle { get; set; }
        public int? IdIntervention { get; set; }
        public string Statut { get; set; }
    }
}

