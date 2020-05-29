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
    public class ProductPaymentsController : Controller
    {
        private readonly DataContext _context;

        public ProductPaymentsController(DataContext context)
        {
            _context = context;
        }

        // GET: ProductPayments
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductPayments.ToListAsync());
        }

        // GET: ProductPayments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productPayment = await _context.ProductPayments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productPayment == null)
            {
                return NotFound();
            }

            return View(productPayment);
        }

        // GET: ProductPayments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductPayments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] ProductPayment productPayment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productPayment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productPayment);
        }

        // GET: ProductPayments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productPayment = await _context.ProductPayments.FindAsync(id);
            if (productPayment == null)
            {
                return NotFound();
            }
            return View(productPayment);
        }

        // POST: ProductPayments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] ProductPayment productPayment)
        {
            if (id != productPayment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productPayment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductPaymentExists(productPayment.Id))
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
            return View(productPayment);
        }

        // GET: ProductPayments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productPayment = await _context.ProductPayments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productPayment == null)
            {
                return NotFound();
            }

            return View(productPayment);
        }

        // POST: ProductPayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productPayment = await _context.ProductPayments.FindAsync(id);
            _context.ProductPayments.Remove(productPayment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductPaymentExists(int id)
        {
            return _context.ProductPayments.Any(e => e.Id == id);
        }
    }
}
