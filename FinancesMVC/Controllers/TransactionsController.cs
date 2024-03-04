using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinancesDomain.Models;
using FinancesMVC;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FinancesMVC.Controllers
{

    public class TransactionsController : Controller
    {
        private readonly Db1Context _context;

        public TransactionsController(Db1Context context)
        {
            _context = context;
        }

        // GET: Transactions
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null) return RedirectToAction("Index", "Categories");

            ViewBag.CategoryId = id;
            ViewBag.CategoryName = name;
            var transactionByCategory = _context.Transactions.Where(t => t.ExpenditureCategoryId == id);
            return View(await transactionByCategory.ToListAsync());
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.CompletedAchievement)
                .Include(t => t.ExpenditureCategory)
                .Include(t => t.Message)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        public IActionResult Create(int? id, string? name)
        {
            ViewBag.CategoryId = id;
            ViewBag.CategoryName = name;
            ViewData["CompletedAchievementId"] = new SelectList(_context.Achievements, "Id", "Id");
            ViewData["ExpenditureCategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["MessageId"] = new SelectList(_context.Messages, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,MoneySpent,BudgetOverflown,MessageId,CompletedAchievementId,ExpenditureCategoryId,ExpenditureNote")] Transaction transaction)
        {
            transaction.Id = _context.Transactions.ToList().Last().Id + 1;
            transaction.UserId = _context.Users.ToList()[0].Id;
            transaction.Date = DateTime.Now;
            var category = await _context.Categories.FindAsync(transaction.ExpenditureCategoryId);
            if (category == null) return NotFound();
            category.TotalExpences += transaction.MoneySpent;
            //check if budget is overflown
            if (category.ExpenditureLimit != null)
            {
                if ((double)category.TotalExpences > category.ExpenditureLimit)
                {
                    category.CategoryColorHexCode = "#d73d23";
                    transaction.BudgetOverflown = true;
                }
            }
            
            _context.Update(category);
            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { category.Id, category.Name});
            }
            ViewData["CompletedAchievementId"] = new SelectList(_context.Achievements, "Id", "Id", transaction.CompletedAchievementId);
            ViewData["ExpenditureCategoryId"] = new SelectList(_context.Categories, "Id", "Name", transaction.ExpenditureCategoryId);
            ViewData["MessageId"] = new SelectList(_context.Messages, "Id", "Id", transaction.MessageId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", transaction.UserId);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id, int? categoryId)
        {
            var category = _context.Categories.Find(categoryId);

            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewBag.Purpose = transaction.ExpenditureNote;
            ViewBag.Date = transaction.Date;
            ViewBag.CategoryId = categoryId;
            ViewBag.CategoryName = category.Name;
            ViewData["CompletedAchievementId"] = new SelectList(_context.Achievements, "Id", "Id", transaction.CompletedAchievementId);
            ViewData["ExpenditureCategoryId"] = new SelectList(_context.Categories, "Id", "Name", transaction.ExpenditureCategoryId);
            ViewData["MessageId"] = new SelectList(_context.Messages, "Id", "Id", transaction.MessageId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", transaction.UserId);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int? categoryId, string? categoryName, [Bind("Id,UserId,MoneySpent,BudgetOverflown,Date,MessageId,CompletedAchievementId,ExpenditureCategoryId,ExpenditureNote")] Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (TryValidateModel(ModelState, nameof(ModelState)))
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Transactions", new { id = categoryId, name = categoryName});
            }
            ViewData["CompletedAchievementId"] = new SelectList(_context.Achievements, "Id", "Id", transaction.CompletedAchievementId);
            ViewData["ExpenditureCategoryId"] = new SelectList(_context.Categories, "Id", "Name", transaction.ExpenditureCategoryId);
            ViewData["MessageId"] = new SelectList(_context.Messages, "Id", "Id", transaction.MessageId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", transaction.UserId);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.CompletedAchievement)
                .Include(t => t.ExpenditureCategory)
                .Include(t => t.Message)
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int? categoryId, string? categoryName)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Transactions", new { id = categoryId, name = categoryName });
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }
    }
}
