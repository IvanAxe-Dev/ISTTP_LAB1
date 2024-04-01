using FinancesMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinancesMVC.Controllers
{
    public class SharedBudgetsController : AuthorizeController
    {
        private readonly Db1Context _context;

        public SharedBudgetsController(Db1Context context)
        {
            _context = context;
        }

        // GET: SharedBudgets
        public async Task<IActionResult> Index()
        {
            var sharedBudgets = _context.SharedBudgets
                .Include(s => s.OwnerUser)
                .Include(p => p.CommonCategory)
                .Where(c => c.OwnerId == IdentityUserId ||
                    (c.AddedUsersId != null && c.AddedUsersId.Contains(IdentityUserId)));

            return View(await sharedBudgets.ToListAsync());
        }

        // GET: SharedBudgets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharedBudget = await _context.SharedBudgets
                .Include(s => s.AddedUsersId)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sharedBudget == null)
            {
                return NotFound();
            }

            return View(sharedBudget);
        }

        // GET: SharedBudgets/Create
        public IActionResult Create()
        {
            ViewData["AddedUserId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // GET: Transactions/Create
        public async Task<IActionResult> NewTransaction(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return RedirectToAction("Create", "Transactions", new { id = category.Id, name = category.Name });
        }

        // POST: SharedBudgets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AddedUserId,CommonCategoryId")] SharedBudget sharedBudget)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sharedBudget);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddedUserId"] = new SelectList(_context.Categories, "Id", "Name", sharedBudget.AddedUsersId);
            return View(sharedBudget);
        }

        // GET: SharedBudgets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharedBudget = await _context.SharedBudgets.FindAsync(id);
            if (sharedBudget == null)
            {
                return NotFound();
            }
            ViewData["AddedUserId"] = new SelectList(_context.Categories, "Id", "Name", sharedBudget.AddedUsersId);
            return View(sharedBudget);
        }

        // POST: SharedBudgets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AddedUserId,CommonCategoryId")] SharedBudget sharedBudget)
        {
            if (id != sharedBudget.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sharedBudget);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SharedBudgetExists(sharedBudget.Id))
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
            ViewData["AddedUserId"] = new SelectList(_context.Categories, "Id", "Name", sharedBudget.AddedUsersId);
            return View(sharedBudget);
        }

        // GET: SharedBudgets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sharedBudget = await _context.SharedBudgets
                .Include(s => s.AddedUsersId)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sharedBudget == null)
            {
                return NotFound();
            }

            return View(sharedBudget);
        }

        // POST: SharedBudgets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sharedBudget = await _context.SharedBudgets.FindAsync(id);
            if (sharedBudget != null)
            {
                _context.SharedBudgets.Remove(sharedBudget);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SharedBudgetExists(int id)
        {
            return _context.SharedBudgets.Any(e => e.Id == id);
        }
    }
}
