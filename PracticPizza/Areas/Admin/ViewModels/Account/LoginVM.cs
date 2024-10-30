using System.ComponentModel.DataAnnotations;

namespace PracticPizza.Areas.Admin.ViewModels
{
    public class LoginVM
    {
        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string UsernameOrEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsRememberedMe { get; set; }
    }
}
