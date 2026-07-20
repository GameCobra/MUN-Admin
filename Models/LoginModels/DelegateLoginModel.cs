using System.ComponentModel.DataAnnotations;

namespace MUNAdmin.Models.LoginModels
{
    public class DelegateLoginModel
    {
        [Required]
        [Display(Name = "MUN Access Code")]
        public int MUNAccessCode { get; set; }

        [Required]
        [Display(Name = "Delegation Access Code")]
        public int DelegationAccsesCode { get; set; }
    }
}
