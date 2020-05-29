using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Xpress.Web.Data;
using Xpress.Web.Data.Entities.Users;

namespace Xpress.Web.Controllers.Users
{
    [Authorize(Roles = "Admin")]
    public class DeliveryGuysController : Controller
    {
        private readonly DataContext _context;

        public DeliveryGuysController(DataContext context)
        {
            _context = context;
        }

        // GET: DeliveryGuys
        public async Task<IActionResult> Index()
        {
            return View(await _context.DeliveryGuys.ToListAsync());
        }

        // GET: DeliveryGuys/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryGuy = await _context.DeliveryGuys
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deliveryGuy == null)
            {
                return NotFound();
            }

            return View(deliveryGuy);
        }

        // GET: DeliveryGuys/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DeliveryGuys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] DeliveryGuy deliveryGuy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deliveryGuy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deliveryGuy);
        }

        // GET: DeliveryGuys/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryGuy = await _context.DeliveryGuys.FindAsync(id);
            if (deliveryGuy == null)
            {
                return NotFound();
            }
            return View(deliveryGuy);
        }

        // POST: DeliveryGuys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] DeliveryGuy deliveryGuy)
        {
            if (id != deliveryGuy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deliveryGuy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryGuyExists(deliveryGuy.Id))
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
            return View(deliveryGuy);
        }

        // GET: DeliveryGuys/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryGuy = await _context.DeliveryGuys
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deliveryGuy == null)
            {
                return NotFound();
            }

            return View(deliveryGuy);
        }

        // POST: DeliveryGuys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deliveryGuy = await _context.DeliveryGuys.FindAsync(id);
            _context.DeliveryGuys.Remove(deliveryGuy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryGuyExists(int id)
        {
            return _context.DeliveryGuys.Any(e => e.Id == id);
        }
    }
}
