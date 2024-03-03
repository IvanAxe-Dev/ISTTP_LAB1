﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinancesDomain.Models;
using FinancesMVC;

namespace FinancesMVC.Controllers
{
    public class SharedBudgetsController : Controller
    {
        private readonly Db1Context _context;

        public SharedBudgetsController(Db1Context context)
        {
            _context = context;
        }

        // GET: SharedBudgets
        public async Task<IActionResult> Index()
        {
            var db1Context = _context.SharedBudgets.Include(s => s.AddedUser).Include(p => p.CommonCategory);
            return View(await db1Context.ToListAsync());
        }

        // GET: SharedBudgets/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var sharedBudget = await _context.SharedBudgets
        //        .Include(s => s.AddedUser)
        //        .Include(s => s.OwnerUser)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (sharedBudget == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(sharedBudget);
        //}

        //// GET: SharedBudgets/Create
        //public IActionResult Create()
        //{
        //    ViewData["AddedUserId"] = new SelectList(_context.Categories, "Id", "Name");
        //    ViewData["OwnerUserId"] = new SelectList(_context.Users, "Id", "Id");
        //    return View();
        //}

        //// POST: SharedBudgets/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,OwnerUserId,AddedUserId,CommonCategoryId")] SharedBudget sharedBudget)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(sharedBudget);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["AddedUserId"] = new SelectList(_context.Categories, "Id", "Name", sharedBudget.AddedUserId);
        //    ViewData["OwnerUserId"] = new SelectList(_context.Users, "Id", "Id", sharedBudget.OwnerUserId);
        //    return View(sharedBudget);
        //}

        //// GET: SharedBudgets/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var sharedBudget = await _context.SharedBudgets.FindAsync(id);
        //    if (sharedBudget == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["AddedUserId"] = new SelectList(_context.Categories, "Id", "Name", sharedBudget.AddedUserId);
        //    ViewData["OwnerUserId"] = new SelectList(_context.Users, "Id", "Id", sharedBudget.OwnerUserId);
        //    return View(sharedBudget);
        //}

        //// POST: SharedBudgets/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,OwnerUserId,AddedUserId,CommonCategoryId")] SharedBudget sharedBudget)
        //{
        //    if (id != sharedBudget.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(sharedBudget);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!SharedBudgetExists(sharedBudget.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["AddedUserId"] = new SelectList(_context.Categories, "Id", "Name", sharedBudget.AddedUserId);
        //    ViewData["OwnerUserId"] = new SelectList(_context.Users, "Id", "Id", sharedBudget.OwnerUserId);
        //    return View(sharedBudget);
        //}

        //// GET: SharedBudgets/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var sharedBudget = await _context.SharedBudgets
        //        .Include(s => s.AddedUser)
        //        .Include(s => s.OwnerUser)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (sharedBudget == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(sharedBudget);
        //}

        //// POST: SharedBudgets/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var sharedBudget = await _context.SharedBudgets.FindAsync(id);
        //    if (sharedBudget != null)
        //    {
        //        _context.SharedBudgets.Remove(sharedBudget);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool SharedBudgetExists(int id)
        //{
        //    return _context.SharedBudgets.Any(e => e.Id == id);
        //}
    }
}
