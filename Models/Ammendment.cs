using System.ComponentModel.DataAnnotations;

namespace MUNAdmin.Models
{
    public class Ammendment
    {
        [Required]
        public int ResolutionID { get; set; }

        public string? ChangeClauseNumber { get; set; }

        [Required]
        public required string NewText { get; set; }
    }
}
