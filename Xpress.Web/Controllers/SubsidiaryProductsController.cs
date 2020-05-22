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
    public class SubsidiaryProductsController : Controller
    {
        private readonly DataContext _context;

        public SubsidiaryProductsController(DataContext context)
        {
            _context = context;
        }

        // GET: SubsidiaryProducts
        public async Task<IActionResult> Index()
        {
            return View(await _context.SubsidiaryProducts.ToListAsync());
        }

        // GET: SubsidiaryProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subsidiaryProduct = await _context.SubsidiaryProducts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subsidiaryProduct == null)
            {
                return NotFound();
            }

            return View(subsidiaryProduct);
        }

        // GET: SubsidiaryProducts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SubsidiaryProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Rating,Description,Barcode,Price,PicturePath,Id,Name")] SubsidiaryProduct subsidiaryProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subsidiaryProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subsidiaryProduct);
        }

        // GET: SubsidiaryProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subsidiaryProduct = await _context.SubsidiaryProducts.FindAsync(id);
            if (subsidiaryProduct == null)
            {
                return NotFound();
            }
            return View(subsidiaryProduct);
        }

        // POST: SubsidiaryProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Rating,Description,Barcode,Price,PicturePath,Id,Name")] SubsidiaryProduct subsidiaryProduct)
        {
            if (id != subsidiaryProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subsidiaryProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubsidiaryProductExists(subsidiaryProduct.Id))
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
            return View(subsidiaryProduct);
        }

        // GET: SubsidiaryProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subsidiaryProduct = await _context.SubsidiaryProducts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subsidiaryProduct == null)
            {
                return NotFound();
            }

            return View(subsidiaryProduct);
        }

        // POST: SubsidiaryProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subsidiaryProduct = await _context.SubsidiaryProducts.FindAsync(id);
            _context.SubsidiaryProducts.Remove(subsidiaryProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubsidiaryProductExists(int id)
        {
            return _context.SubsidiaryProducts.Any(e => e.Id == id);
        }
    }
}
