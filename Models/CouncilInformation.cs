using System.ComponentModel.DataAnnotations;

namespace MUNAdmin.Models
{
    public class CouncilInformation
    {
        [Required]
        public required string CouncilName { get; set; }
    }
}
