using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PerfumeStore.Data;
using PerfumeStore.Helpers;
using PerfumeStore.Models;

namespace PerfumeStore.Controllers
{
    public class ProductsController : Controller
    {
        private readonly PerfumeStoreContext _context;

        public ProductsController(PerfumeStoreContext context)
        {
            _context = context;
        }

        public MyViewModel GetAllProducts(int page)
        {
            var query = _context.Products.AsNoTracking().AsQueryable();

            var productsCount = query.Count();
            var pagemax = 6;
            int totalPages = (int)Math.Ceiling((decimal)productsCount / pagemax);
            return new MyViewModel
            {
                CurrentPage = page,
                TotalPages = totalPages,
                Products = query
                    .Skip((page - 1) * pagemax).Take(totalPages).ToList(),

            };
        }


        // GET: Products
        public  IActionResult Index(int page = 1)
        {
            var vm = GetAllProducts(page);
            return View(vm);

            //if (_context.Products != null)
            //{
            //    var products =  _context.Products.AsNoTracking().AsQueryable();
            //    var productsCount = products.Count();
            //    var pagemax = 6;

            //    var viewModel =(int)Math.Ceiling((decimal)productsCount / pagemax);
            //    new MyViewModel
            //    {
            //        Products = products.Skip((page - 1) * pagemax).Take(totalPages).ToList(),
            //        CurrentPage = page,
            //        TotalPages = totalPages,
            //    };
            //    return View(viewModel);

            //    //return View(await _context.Products.ToListAsync());
            //}
            //return Problem("Entity set 'PerfumeStoreContext.Product'  is null.");


        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Stock)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.Stocks, "Id", "Id");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Tag,Size")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Stocks, "Id", "Id", product.Id);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Stocks, "Id", "Id", product.Id);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Tag,Size")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Stocks, "Id", "Id", product.Id);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Stock)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'PerfumeStoreContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
