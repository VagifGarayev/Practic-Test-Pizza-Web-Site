using System.ComponentModel.DataAnnotations;

namespace PracticPizza.Areas.Admin.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        public string Username { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]

        public string ConfrimPassword { get; set; }
    }
}
