using System.ComponentModel.DataAnnotations;

namespace MUNAdmin.Models.LoginModels
{
    public class DelegateLoginModel
    {
        [Required]
        public int MUNAccessCode { get; set; }

        [Required]
        public int DelegationAccsesCode { get; set; }
    }
}
