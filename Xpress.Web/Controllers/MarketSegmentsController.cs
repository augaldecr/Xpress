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
    public class MarketSegmentsController : Controller
    {
        private readonly DataContext _context;

        public MarketSegmentsController(DataContext context)
        {
            _context = context;
        }

        // GET: MarketSegments
        public async Task<IActionResult> Index()
        {
            return View(await _context.MarketSegments.ToListAsync());
        }

        // GET: MarketSegments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marketSegment = await _context.MarketSegments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marketSegment == null)
            {
                return NotFound();
            }

            return View(marketSegment);
        }

        // GET: MarketSegments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MarketSegments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] MarketSegment marketSegment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(marketSegment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(marketSegment);
        }

        // GET: MarketSegments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marketSegment = await _context.MarketSegments.FindAsync(id);
            if (marketSegment == null)
            {
                return NotFound();
            }
            return View(marketSegment);
        }

        // POST: MarketSegments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] MarketSegment marketSegment)
        {
            if (id != marketSegment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(marketSegment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarketSegmentExists(marketSegment.Id))
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
            return View(marketSegment);
        }

        // GET: MarketSegments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marketSegment = await _context.MarketSegments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marketSegment == null)
            {
                return NotFound();
            }

            return View(marketSegment);
        }

        // POST: MarketSegments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var marketSegment = await _context.MarketSegments.FindAsync(id);
            _context.MarketSegments.Remove(marketSegment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MarketSegmentExists(int id)
        {
            return _context.MarketSegments.Any(e => e.Id == id);
        }
    }
}
