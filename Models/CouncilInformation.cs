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
        public int? CurrentResolutionID { get; set; } = null;

        // Visual Data
        public required string CouncilName { get; set; }
        public required string GradientPrimaryColor { get; set; }
        public required string GradientSecondaryColor { get; set; }
        public bool IsInSession { get; set; }
        public string? CurrentActivity { get; set; } = null;
        public string? CurrentSpeakingCountry { get; set; } = null;
        public string? RebuttalingCountry { get; set; } = null;
    }
}
