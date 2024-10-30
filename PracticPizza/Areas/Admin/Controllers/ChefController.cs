using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticPizza.Areas.Admin.ViewModels;
using PracticPizza.DAL;
using PracticPizza.Models;
using PracticPizza.Utilities.Extension;

namespace PracticPizza.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles ="Admin")]
	public class ChefController : Controller
	{
		private readonly AppDbContext _context;
		private readonly IWebHostEnvironment _env;

		public ChefController(AppDbContext context,IWebHostEnvironment env)
		{
			_context = context;
		_env = env;
		}
		public async Task<IActionResult> Index()
		{
			List<Chef> chefs = await _context.Chef.ToListAsync();

			return View(chefs);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]

		public async Task<IActionResult> Create(CreateChefVM chefVM)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}

			if (!chefVM.Photo.FileType("image/"))
			{
				ModelState.AddModelError("Photo", "Sekil tipi uygun deyil");
				return View();
			}

			if (!chefVM.Photo.FileSize(3 * 1024))
			{
				ModelState.AddModelError("Photo", "sekil olcusu uygun deyil");
				return View();
			}

			string fileName = await chefVM.Photo.CreateFile(_env.WebRootPath, "uploads");

			Chef chef = new Chef
			{
				Name = chefVM.Name,
				Image = fileName,
				Department = chefVM.Department,
			};

			await _context.Chef.AddAsync(chef);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));

		}


		public async Task<IActionResult> Update(int id)
		{
			if (id <= 0) return BadRequest();

			Chef chef =await _context.Chef.FirstOrDefaultAsync(c => c.Id == id);

			if(chef == null) return NotFound();

			UpdateChefVM chefVM = new UpdateChefVM
			{
				Name= chef.Name,
				Department= chef.Department,
				Image= chef.Image,
			};

			return View(chefVM);	

		}

		[HttpPost]

		public async Task<IActionResult> Update(int id,UpdateChefVM chefVM)
		{
			if(id<=0) return BadRequest();

			Chef chef = await _context.Chef.FirstOrDefaultAsync(che => che.Id == id);
			if(chef == null) return NotFound();

			if(chefVM.Photo is not null)
			{
				if (!chefVM.Photo.FileType("image/"))
				{
					ModelState.AddModelError("Photo", "sekil tipi uygun deyil");
					return View(chefVM);
				}

				if (!chefVM.Photo.FileSize(3 * 1024))
				{
					ModelState.AddModelError("Photo", "Sekil olcusu uygun deyil");
					return View(chefVM);
				}

				string newImage = await chefVM.Photo.CreateFile(_env.WebRootPath, "uploads");
				chef.Image.Delete(_env.WebRootPath, "uploads");
				chef.Image = newImage;
			}

			chef.Name= chefVM.Name;
			chef.Department= chefVM.Department;


			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}


		public async Task<IActionResult> Delete(int id)
		{
			if(id<=0)  return BadRequest(); 

			Chef chef =await _context.Chef.FirstOrDefaultAsync(c => c.Id == id);

			if(chef == null) return NotFound();

			chef.Image.Delete(_env.WebRootPath, "uploads");

			_context.Chef.Remove(chef);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}
	}
}
