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
    public class SubsidiaryAdminsController : Controller
    {
        private readonly DataContext _context;

        public SubsidiaryAdminsController(DataContext context)
        {
            _context = context;
        }

        // GET: SubsidiaryAdmins
        public async Task<IActionResult> Index()
        {
            return View(await _context.SubsidiaryAdmins.ToListAsync());
        }

        // GET: SubsidiaryAdmins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subsidiaryAdmin = await _context.SubsidiaryAdmins
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subsidiaryAdmin == null)
            {
                return NotFound();
            }

            return View(subsidiaryAdmin);
        }

        // GET: SubsidiaryAdmins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SubsidiaryAdmins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] SubsidiaryAdmin subsidiaryAdmin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subsidiaryAdmin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subsidiaryAdmin);
        }

        // GET: SubsidiaryAdmins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subsidiaryAdmin = await _context.SubsidiaryAdmins.FindAsync(id);
            if (subsidiaryAdmin == null)
            {
                return NotFound();
            }
            return View(subsidiaryAdmin);
        }

        // POST: SubsidiaryAdmins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] SubsidiaryAdmin subsidiaryAdmin)
        {
            if (id != subsidiaryAdmin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subsidiaryAdmin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubsidiaryAdminExists(subsidiaryAdmin.Id))
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
            return View(subsidiaryAdmin);
        }

        // GET: SubsidiaryAdmins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subsidiaryAdmin = await _context.SubsidiaryAdmins
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subsidiaryAdmin == null)
            {
                return NotFound();
            }

            return View(subsidiaryAdmin);
        }

        // POST: SubsidiaryAdmins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subsidiaryAdmin = await _context.SubsidiaryAdmins.FindAsync(id);
            _context.SubsidiaryAdmins.Remove(subsidiaryAdmin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubsidiaryAdminExists(int id)
        {
            return _context.SubsidiaryAdmins.Any(e => e.Id == id);
        }
    }
}
