using Microsoft.EntityFrameworkCore;

namespace MUNAdmin.Models
{
    public class DelegationCouncil
    {
        public int CouncilId { get; set; }

        public required CouncilInformation Council { get; set; }
        public int AmendmentPoints { get; set; }
        public int RebuttalPoints { get; set; }

        public bool RequestedRebuttal { get; set; }

        public List<Ammendment> Ammendments { get; set; } = [];
    }
}
