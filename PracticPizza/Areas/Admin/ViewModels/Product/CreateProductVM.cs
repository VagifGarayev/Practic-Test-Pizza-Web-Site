using System.ComponentModel.DataAnnotations;

namespace PracticPizza.Areas.Admin.ViewModels
{
	public class CreateProductVM
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public decimal Price { get; set; }
		public IFormFile Photo { get; set; }
	}
}
