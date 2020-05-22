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
    public class FranchiseTypesController : Controller
    {
        private readonly DataContext _context;

        public FranchiseTypesController(DataContext context)
        {
            _context = context;
        }

        // GET: FranchiseTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.FranchiseTypes.ToListAsync());
        }

        // GET: FranchiseTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var franchiseType = await _context.FranchiseTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (franchiseType == null)
            {
                return NotFound();
            }

            return View(franchiseType);
        }

        // GET: FranchiseTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FranchiseTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] FranchiseType franchiseType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(franchiseType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(franchiseType);
        }

        // GET: FranchiseTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var franchiseType = await _context.FranchiseTypes.FindAsync(id);
            if (franchiseType == null)
            {
                return NotFound();
            }
            return View(franchiseType);
        }

        // POST: FranchiseTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] FranchiseType franchiseType)
        {
            if (id != franchiseType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(franchiseType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FranchiseTypeExists(franchiseType.Id))
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
            return View(franchiseType);
        }

        // GET: FranchiseTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var franchiseType = await _context.FranchiseTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (franchiseType == null)
            {
                return NotFound();
            }

            return View(franchiseType);
        }

        // POST: FranchiseTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var franchiseType = await _context.FranchiseTypes.FindAsync(id);
            _context.FranchiseTypes.Remove(franchiseType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FranchiseTypeExists(int id)
        {
            return _context.FranchiseTypes.Any(e => e.Id == id);
        }
    }
}
