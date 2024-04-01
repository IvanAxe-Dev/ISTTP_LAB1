using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinancesMVC.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using FinancesMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace FinancesMVC.Controllers
{
    public class TransactionsController : AuthorizeController
    {
        private readonly Db1Context _context;

        public TransactionsController(Db1Context context)
        {
            _context = context;
        }

        // GET: Transactions
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null || name == null) return RedirectToAction("Index", "Categories");

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
        public IActionResult Create(int? id, bool isShared)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                ViewBag.CategoryId = id;
                ViewBag.CategoryName = category.Name;
                ViewBag.ExpensesLeft =
                (decimal)category.ExpenditureLimit -
                _context.Transactions
                .Where(t => t.ExpenditureCategoryId == id)
                .Sum(t => t.MoneySpent);
            }
            ViewData["CompletedAchievementId"] = new SelectList(_context.Achievements, "Id", "Id");
            ViewData["ExpenditureCategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["MessageId"] = new SelectList(_context.Messages, "Id", "Id");
            ViewData["UserId"] = IdentityUserId;
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MoneySpent,BudgetOverflown,MessageId,CompletedAchievementId,ExpenditureCategoryId,ExpenditureNote")] Transaction transaction)
        {
            transaction.Date = DateTime.Now;
            var category = await _context.Categories.FindAsync(transaction.ExpenditureCategoryId);
            if (category == null) return NotFound();
            //check if budget is overflown
            if (category.ExpenditureLimit != null)
            {
                if ((double)category.TotalExpences > category.ExpenditureLimit)
                {
                    transaction.BudgetOverflown = true;
                }
            }

            _context.Update(category);
            if (ModelState.IsValid)
            {
                transaction.UserId = IdentityUserId;
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { category.Id, category.Name });
            }
            ViewData["CompletedAchievementId"] = new SelectList(_context.Achievements, "Id", "Id", transaction.CompletedAchievementId);
            ViewData["ExpenditureCategoryId"] = new SelectList(_context.Categories, "Id", "Name", transaction.ExpenditureCategoryId);
            ViewData["MessageId"] = new SelectList(_context.Messages, "Id", "Id", transaction.MessageId);
            ViewData["UserId"] = IdentityUserId;
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

            if (category == null)
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
            ViewData["UserId"] = IdentityUserId;
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int? categoryId, string? categoryName, [Bind("Id,UserId,MoneySpent,BudgetOverflown,Date,MessageId,CompletedAchievementId,ExpenditureCategoryId,ExpenditureNote")] Transaction transaction)
        {
            var category = _context.Categories.Find(categoryId);

            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (category == null)
            {
                return NotFound();
            }

            ModelState.Clear();
            TryValidateModel(transaction);

            if (ModelState.IsValid)
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
                return RedirectToAction("Index", "Transactions", new { id = categoryId, name = categoryName });
            }
            ViewBag.Purpose = transaction.ExpenditureNote;
            ViewBag.Date = transaction.Date;
            ViewBag.CategoryId = categoryId;
            ViewBag.CategoryName = category.Name;
            ViewData["CompletedAchievementId"] = new SelectList(_context.Achievements, "Id", "Id", transaction.CompletedAchievementId);
            ViewData["ExpenditureCategoryId"] = new SelectList(_context.Categories, "Id", "Name", transaction.ExpenditureCategoryId);
            ViewData["MessageId"] = new SelectList(_context.Messages, "Id", "Id", transaction.MessageId);
            ViewData["UserId"] = IdentityUserId;
            return View(transaction);
        }

        public async Task<IActionResult> DeleteCategory(int? id)
        {
            TempData["id"] = id;
            return RedirectToAction("Delete", "Categories", new { id });
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

            var category = await _context.Categories.FindAsync(categoryId);
            if (category != null)
            {
                _context.Categories.Update(category);
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
