using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;


namespace MUNAdmin.Models
{
    public class MUNInstance
    {
        public int Id { get; set; }

        [Required]
        public required string AdminUsername { get; set; }

        [Required]
        public required string AdminPassword { get; set; }

        [Required]
        public required string MUNTitle { get; set; }

        public int MUNAccessCode { get; set; }

        public List<DelegationInstance> DelegationList { get; set; } = [];

        public List<CouncilInformation> CouncilInformationList { get; set; } = [];
    }
}
