namespace MUNAdmin.Models
{
    public class DelegationCouncil
    {
        public required CouncilInformation Council { get; set; }
        public int AmendmentPoints { get; set; }
        public int RebuttalPoints { get; set; }

        public bool RequestedRebuttal { get; set; }
    }
}
