using System.ComponentModel.DataAnnotations;

namespace MUNAdmin.Models
{
    public class CouncilInformation
    {
        //Critical Database Information
        public int Id { get; set; }
        public int MUNInstanceId { get; set; }

        //Critical Functional Information
        [Required]
        public List<Resolution> Resolutions { get; set; } = [];
        public bool AcceptingRebuttals { get; set; } 

        // Visual Data
        public required string CouncilName { get; set; }
        public required string GradientPrimaryColor { get; set; }
        public required string GradientSecondaryColor { get; set; }
        public bool IsInSession { get; set; }
    }
}
