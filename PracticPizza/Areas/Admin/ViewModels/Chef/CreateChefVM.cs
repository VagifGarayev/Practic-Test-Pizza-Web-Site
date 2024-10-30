
using System.ComponentModel.DataAnnotations;

namespace PracticPizza.Areas.Admin.ViewModels
{
	public class CreateChefVM
	{
		[Required]
		[MinLength(3)]
		[MaxLength(25)]
		public string Name { get; set; }
		[Required]
		[MinLength(3)]
		[MaxLength(25)]
		public string Department { get; set; }

		public IFormFile Photo { get; set; }
	}
}
