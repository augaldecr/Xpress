using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Xpress.Web.Data;
using Xpress.Web.Data.Entities.Payments;

namespace Xpress.Web.Controllers.Payments
{
    public class DeliveryPaymentsController : Controller
    {
        private readonly DataContext _context;

        public DeliveryPaymentsController(DataContext context)
        {
            _context = context;
        }

        // GET: DeliveryPayments
        public async Task<IActionResult> Index()
        {
            return View(await _context.DeliveryPayments.ToListAsync());
        }

        // GET: DeliveryPayments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryPayment = await _context.DeliveryPayments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deliveryPayment == null)
            {
                return NotFound();
            }

            return View(deliveryPayment);
        }

        // GET: DeliveryPayments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DeliveryPayments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] DeliveryPayment deliveryPayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deliveryPayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deliveryPayment);
        }

        // GET: DeliveryPayments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryPayment = await _context.DeliveryPayments.FindAsync(id);
            if (deliveryPayment == null)
            {
                return NotFound();
            }
            return View(deliveryPayment);
        }

        // POST: DeliveryPayments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] DeliveryPayment deliveryPayment)
        {
            if (id != deliveryPayment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deliveryPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryPaymentExists(deliveryPayment.Id))
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
            return View(deliveryPayment);
        }

        // GET: DeliveryPayments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryPayment = await _context.DeliveryPayments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deliveryPayment == null)
            {
                return NotFound();
            }

            return View(deliveryPayment);
        }

        // POST: DeliveryPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deliveryPayment = await _context.DeliveryPayments.FindAsync(id);
            _context.DeliveryPayments.Remove(deliveryPayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryPaymentExists(int id)
        {
            return _context.DeliveryPayments.Any(e => e.Id == id);
        }
    }
}
