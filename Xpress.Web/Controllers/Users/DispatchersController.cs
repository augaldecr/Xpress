using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Xpress.Web.Data;
using Xpress.Web.Data.Entities.Users;

namespace Xpress.Web.Controllers.Users
{
    public class DispatchersController : Controller
    {
        private readonly DataContext _context;

        public DispatchersController(DataContext context)
        {
            _context = context;
        }

        // GET: Dispatchers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dispatchers.ToListAsync());
        }

        // GET: Dispatchers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dispatcher = await _context.Dispatchers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dispatcher == null)
            {
                return NotFound();
            }

            return View(dispatcher);
        }

        // GET: Dispatchers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dispatchers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] Dispatcher dispatcher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dispatcher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dispatcher);
        }

        // GET: Dispatchers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dispatcher = await _context.Dispatchers.FindAsync(id);
            if (dispatcher == null)
            {
                return NotFound();
            }
            return View(dispatcher);
        }

        // POST: Dispatchers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] Dispatcher dispatcher)
        {
            if (id != dispatcher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dispatcher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DispatcherExists(dispatcher.Id))
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
            return View(dispatcher);
        }

        // GET: Dispatchers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dispatcher = await _context.Dispatchers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dispatcher == null)
            {
                return NotFound();
            }

            return View(dispatcher);
        }

        // POST: Dispatchers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dispatcher = await _context.Dispatchers.FindAsync(id);
            _context.Dispatchers.Remove(dispatcher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DispatcherExists(int id)
        {
            return _context.Dispatchers.Any(e => e.Id == id);
        }
    }
}
