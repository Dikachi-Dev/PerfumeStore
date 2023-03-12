using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
                    .Skip((page - 1) * pagemax).Take(pagemax).ToList()

                };
                string message = TempData["Message"] as string;
                if (!string.IsNullOrEmpty(message))
                {
                    ViewBag.Message = message;
                    TempData.Remove("Message");
                }
                return View(viewModel);
            }
            return Problem("Entity set 'PerfumeStoreContext.Product'  is null.");        
        }
        public IActionResult AddToCart(int productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return NotFound();
            }

            var cart = Request.Cookies["cart"];
            if (cart != null)
            {
                var cartList = JsonConvert.DeserializeObject<List<CartItem>>(cart);
                var existingItem = cartList.FirstOrDefault(item => item.ProductId == productId);
                if (existingItem != null)
                {
                    existingItem.Quantity++;
                }
                else
                {
                    cartList.Add(new CartItem
                    {
                        ProductId = productId,
                        Name = product.Name,
                        Price = product.Price,
                        Quantity = 1
                    });
                }
                Response.Cookies.Append("cart", JsonConvert.SerializeObject(cartList));
            }
            else
            {
                var cartList = new List<CartItem>
        {
            new CartItem
            {
                ProductId = productId,
                Name = product.Name,
                Price = product.Price,
                Quantity = 1
            }
        };
                Response.Cookies.Append("cart", JsonConvert.SerializeObject(cartList));
            }
            TempData["Message"] = "Continue";
            return RedirectToAction("Index");
        }
        public IActionResult Cart()
        {
            var cart = Request.Cookies["cart"];
            if (cart != null)
            {
                var cartList = JsonConvert.DeserializeObject<List<CartItem>>(cart);
                ViewBag.Cart = cartList;
                return View();
            }
            return View();
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