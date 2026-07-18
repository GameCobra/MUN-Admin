using System.ComponentModel.DataAnnotations;

namespace MUNAdmin.Models
{
    public class DelegationInstance
    {
        public int Id { get; set; }

        [Required]
        public required string DelegationCountry { get; set; }

        public int DelegationAccsesCode { get; set; }
        public required List<DelegationCouncil> CouncilList { get; set; }

    }
}
