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
    public class DeliveryDetailsController : Controller
    {
        private readonly DataContext _context;

        public DeliveryDetailsController(DataContext context)
        {
            _context = context;
        }

        // GET: DeliveryDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.DeliveryDetails.ToListAsync());
        }

        // GET: DeliveryDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryDetail = await _context.DeliveryDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deliveryDetail == null)
            {
                return NotFound();
            }

            return View(deliveryDetail);
        }

        // GET: DeliveryDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DeliveryDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Latitude,Longitude")] DeliveryDetail deliveryDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deliveryDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deliveryDetail);
        }

        // GET: DeliveryDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryDetail = await _context.DeliveryDetails.FindAsync(id);
            if (deliveryDetail == null)
            {
                return NotFound();
            }
            return View(deliveryDetail);
        }

        // POST: DeliveryDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Latitude,Longitude")] DeliveryDetail deliveryDetail)
        {
            if (id != deliveryDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deliveryDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryDetailExists(deliveryDetail.Id))
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
            return View(deliveryDetail);
        }

        // GET: DeliveryDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryDetail = await _context.DeliveryDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deliveryDetail == null)
            {
                return NotFound();
            }

            return View(deliveryDetail);
        }

        // POST: DeliveryDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deliveryDetail = await _context.DeliveryDetails.FindAsync(id);
            _context.DeliveryDetails.Remove(deliveryDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryDetailExists(int id)
        {
            return _context.DeliveryDetails.Any(e => e.Id == id);
        }
    }
}
