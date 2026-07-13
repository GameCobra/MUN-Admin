using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;


namespace MUNAdmin.Models
{
    public class MUNInstanceModel
    {
        public int Id { get; set; }

        [Required]
        public required string AdminUsername { get; set; }

        [Required]
        public required string AdminPassword { get; set; }

        [Required]
        public required string MUNTitle { get; set; }

        public int MUNAccessCode { get; set; }

        public required List<DelegationInstance> DelegationList { get; set; }

        public required List<CouncilInformation> CouncilInformationList { get; set; }


    }
    public class DelegationInstance
    {
        [Required]
        public required string DelegationCountry { get; set; }

        public int DelegationAccsesCode { get; set; }
        public required List<DelegationCouncil> CouncilList { get; set; }

    }

    public class DelegationCouncil
    {
        public required CouncilInformation Council { get; set; }
        public int AmendmentPoints { get; set; }
        public int RebuttalPoints { get; set; }
        
        public bool RequestedRebuttal { get; set; }
    }

    public class CouncilInformation
    {
        [Required]
        public required string CouncilName { get; set; }
    }

    public class Ammendment
    {
        [Required]
        public int ResolutionID { get; set; }

        public string? ChangeClauseNumber { get; set; }

        [Required]
        public required string NewText { get; set; }
    }
}
