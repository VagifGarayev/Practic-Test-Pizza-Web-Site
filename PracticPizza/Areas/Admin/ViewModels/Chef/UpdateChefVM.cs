namespace PracticPizza.Areas.Admin.ViewModels
{
	public class UpdateChefVM
	{
		public string Name { get; set; }
		public string Department { get; set; }
		public string Image { get; set; }
		public IFormFile? Photo { get; set; }
	}
}
