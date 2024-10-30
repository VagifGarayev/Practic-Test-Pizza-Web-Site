using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticPizza.DAL;
using PracticPizza.Models;
using PracticPizza.ViewModels;

namespace PracticPizza.Contollers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
           _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Product> products= await _context.Products.ToListAsync();
            List<Chef> chefs= await _context.Chef.ToListAsync();

            HomeVM home= new HomeVM
            {
                Products=products,
                Chef=chefs
            };

            return View(home);
        }
    }
}
