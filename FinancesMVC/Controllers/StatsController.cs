using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinancesMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace FinancesMVC.Controllers
{
    public class StatsController : AuthorizeController
    {
        private readonly Db1Context _context;

        public StatsController(Db1Context context)
        {
            _context = context;
        }

        // GET: Stats
        public async Task<IActionResult> Index()
        {
            var db1Context = _context.Stats.Include(s => s.ChosenCategory)
                .Include(s => s.User)
                .Where(c => c.UserId == IdentityUserId);
            return View(await db1Context.ToListAsync());
        }

        // GET: Stats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stat = await _context.Stats
                .Include(s => s.ChosenCategory)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stat == null)
            {
                return NotFound();
            }

            return View(stat);
        }

        // GET: Stats/Create
        public IActionResult Create()
        {
            ViewData["ChosenCategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Stats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ChosenCategoryId,StartTime,EndTime,CalculatedExpances")] Stat stat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChosenCategoryId"] = new SelectList(_context.Categories, "Id", "Name", stat.ChosenCategoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", stat.UserId);
            return View(stat);
        }

        // GET: Stats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stat = await _context.Stats.FindAsync(id);
            if (stat == null)
            {
                return NotFound();
            }
            ViewData["ChosenCategoryId"] = new SelectList(_context.Categories, "Id", "Name", stat.ChosenCategoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", stat.UserId);
            return View(stat);
        }

        // POST: Stats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,ChosenCategoryId,StartTime,EndTime,CalculatedExpances")] Stat stat)
        {
            if (id != stat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatExists(stat.Id))
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
            ViewData["ChosenCategoryId"] = new SelectList(_context.Categories, "Id", "Name", stat.ChosenCategoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", stat.UserId);
            return View(stat);
        }

        // GET: Stats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stat = await _context.Stats
                .Include(s => s.ChosenCategory)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stat == null)
            {
                return NotFound();
            }

            return View(stat);
        }

        // POST: Stats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stat = await _context.Stats.FindAsync(id);
            if (stat != null)
            {
                _context.Stats.Remove(stat);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatExists(int id)
        {
            return _context.Stats.Any(e => e.Id == id);
        }
    }
}
