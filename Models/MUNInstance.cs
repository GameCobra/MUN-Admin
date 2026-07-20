using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;


namespace MUNAdmin.Models
{
    public class MUNInstance
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Admin Username")]
        public required string AdminUsername { get; set; }

        [Required]
        [Display(Name = "Admin Password")]
        public required string AdminPassword { get; set; }

        [Required]
        [Display(Name = "MUN Title")]
        public required string MUNTitle { get; set; }

        [Display(Name = "MUN Access Code")]
        public int MUNAccessCode { get; set; }

        public List<DelegationInstance> DelegationList { get; set; } = [];

        public List<CouncilInformation> CouncilInformationList { get; set; } = [];
    }
}
