using System.ComponentModel.DataAnnotations;

namespace PracticPizza.Areas.Admin.ViewModels
{
	public class UpdateProductVM
	{
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Image { get; set; }
        public IFormFile? Photo { get; set; }
    }
}
