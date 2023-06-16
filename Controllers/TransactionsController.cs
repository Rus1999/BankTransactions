using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BankTransactions.Models;

namespace BankTransactions.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly TransactionsDbContext _context;

        public TransactionsController(TransactionsDbContext context)
        {
            _context = context;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
              return _context.Transactions != null ? 
                          View(await _context.Transactions.ToListAsync()) :
                          Problem("Entity set 'TransactionsDbContext.Transactions'  is null.");
        }

        // GET: Transactions/AddOrEdit
        public IActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new Transactions());
            else
                return View(_context.Transactions.Find(id));
        }

        // POST: Transactions/AddOrEdit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit([Bind("TransactionId,AccountNumber,BeneficiaryName,BankName,SWIFTCode,Amount,Date")] Transactions transactions)
        {
            if (ModelState.IsValid)
            {
                if(transactions.TransactionId == 0)
                {
                    transactions.Date = DateTime.Now;
                    _context.Add(transactions);
                }
                else
                {
                    _context.Update(transactions);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transactions);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transactions == null)
            {
                return Problem("Entity set 'TransactionsDbContext.Transactions'  is null.");
            }
            var transactions = await _context.Transactions.FindAsync(id);
            if (transactions != null)
            {
                _context.Transactions.Remove(transactions);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
