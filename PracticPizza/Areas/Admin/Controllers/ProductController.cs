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
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Product> products = await _context.Products.ToListAsync();

            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(CreateProductVM productVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!productVM.Photo.FileType("image/"))
            {
                ModelState.AddModelError("Photo", "Sekil tipi uygun deyil ");
                return View();
            }

            if (!productVM.Photo.FileSize(5 * 1024))
            {
                ModelState.AddModelError("Photo", "Sekil tipi uygun deyil ");
                return View();
            }

            string fileName = await productVM.Photo.CreateFile(_env.WebRootPath, "uploads");

            Product product = new Product
            {
                Name = productVM.Name,
                Price = productVM.Price,
                Image = fileName
            };


            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();

            Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();

            UpdateProductVM productVM = new UpdateProductVM
            {
                Name = product.Name,
                Price = product.Price,
                Image = product.Image,
            };

            return View(productVM);

        }


        [HttpPost]

        public async Task<IActionResult> Update(int id, UpdateProductVM productVM)
        {
            if(!ModelState.IsValid) return View(productVM);

            if (id <= 0) return BadRequest();

            Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();

            if (productVM.Photo is not null)
            {

                if (!productVM.Photo.FileType("image/"))
                {
                    ModelState.AddModelError("Photo", "Sekil tipi uygun deyil ");
                    return View();
                }

                if (!productVM.Photo.FileSize(5 * 1024))
                {
                    ModelState.AddModelError("Photo", "Sekil tipi uygun deyil ");
                    return View();
                }

                string newImage = await productVM.Photo.CreateFile(_env.WebRootPath, "uploads");
                product.Image.Delete(_env.WebRootPath, "uploads");
                product.Image = newImage;
            }


            product.Name = productVM.Name;
            product.Price = productVM.Price;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            Product product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null) return NotFound();

            product.Image.Delete(_env.WebRootPath, "uploads");


            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
   