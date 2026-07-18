using System.ComponentModel.DataAnnotations;

namespace MUNAdmin.Models.LoginModels
{
    public class AdminLoginModel
    {
        [Required]
        public required string AdminUsername { get; set; }

        [Required]
        public required string AdminPassword { get; set; }
    }
}
