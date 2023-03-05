using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerfumeStore.Data;
using PerfumeStore.Helpers;
using PerfumeStore.Models;
using System.Diagnostics;

namespace PerfumeStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PerfumeStoreContext _context;

        public HomeController(ILogger<HomeController> logger, PerfumeStoreContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(int page = 1)
        {

            if (_context.Products != null)
            {
                var products = _context.Products.AsNoTracking().AsQueryable();
                var productsCount = products.Count();
                var pagemax = 6;
                int totalPages = (int)Math.Ceiling((decimal)productsCount / pagemax);
                var viewModel = new ProductsViewModel
                {
                    CurrentPage = page,
                    TotalPages = totalPages,
                    Products = products
                        .Skip((page - 1) * pagemax).Take(totalPages).ToList()
                };              
                return View(viewModel);
            }
            return Problem("Entity set 'PerfumeStoreContext.Product'  is null.");        
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}