namespace ClaimTrack.NetBackend.Models
{
    public class Intervention
    {
        public int Id { get; set; }

        public Reclamation Reclamation { get; set; }
    }
}
