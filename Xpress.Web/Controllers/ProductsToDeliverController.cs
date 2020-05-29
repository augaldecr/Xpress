using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Xpress.Web.Data;
using Xpress.Web.Data.Entities;

namespace Xpress.Web.Controllers
{
    public class ProductsToDeliverController : Controller
    {
        private readonly DataContext _context;

        public ProductsToDeliverController(DataContext context)
        {
            _context = context;
        }

        // GET: ProductsToDeliver
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductsToDeliver.ToListAsync());
        }

        // GET: ProductsToDeliver/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productToDeliver = await _context.ProductsToDeliver
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productToDeliver == null)
            {
                return NotFound();
            }

            return View(productToDeliver);
        }

        // GET: ProductsToDeliver/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductsToDeliver/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] ProductToDeliver productToDeliver)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productToDeliver);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productToDeliver);
        }

        // GET: ProductsToDeliver/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productToDeliver = await _context.ProductsToDeliver.FindAsync(id);
            if (productToDeliver == null)
            {
                return NotFound();
            }
            return View(productToDeliver);
        }

        // POST: ProductsToDeliver/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] ProductToDeliver productToDeliver)
        {
            if (id != productToDeliver.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productToDeliver);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductToDeliverExists(productToDeliver.Id))
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
            return View(productToDeliver);
        }

        // GET: ProductsToDeliver/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productToDeliver = await _context.ProductsToDeliver
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productToDeliver == null)
            {
                return NotFound();
            }

            return View(productToDeliver);
        }

        // POST: ProductsToDeliver/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productToDeliver = await _context.ProductsToDeliver.FindAsync(id);
            _context.ProductsToDeliver.Remove(productToDeliver);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductToDeliverExists(int id)
        {
            return _context.ProductsToDeliver.Any(e => e.Id == id);
        }
    }
}
