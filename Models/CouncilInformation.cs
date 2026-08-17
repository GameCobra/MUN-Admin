using System.ComponentModel.DataAnnotations;

namespace MUNAdmin.Models
{
    public class CouncilInformation
    {
        public int Id { get; set; }
        public int MUNInstanceId { get; set; }

        [Required]
        public required string CouncilName { get; set; }

        public required string PrimaryColor { get; set; }
        public required string SecondaryColor { get; set; }

        public List<Resolution> Resolutions { get; set; } = [];
    }
}
