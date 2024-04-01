using FinancesMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FinancesMVC.Controllers
{
    public class UserProfileController : AuthorizeController
    {
        private readonly Db1Context _context;

        public UserProfileController(Db1Context context)
        {
            _context = context;
        }

        // GET: UserProfiles
        public async Task<IActionResult> Index()
        {
            var db1Context = _context.UserProfiles
                .Include(u => u.User)
                .Where(c => c.UserId == IdentityUserId);

            return View(await db1Context.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create()
        {
            UserProfile userProfile = new() { UserId = IdentityUserId};

            if (ModelState.IsValid)
            {
                _context.Add(userProfile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(userProfile);
        }

        // GET: UserProfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProfile = await _context.UserProfiles
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userProfile == null)
            {
                return NotFound();
            }

            return View(userProfile);
        }

        // GET: UserProfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProfile = await _context.UserProfiles.FindAsync(id);
            if (userProfile == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userProfile.UserId);
            return View(userProfile);
        }

        // POST: UserProfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,FirstName,LastName,Birthdate,Country,AboutMe")] UserProfile userProfile)
        {
            if (id != userProfile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserProfileExists(userProfile.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userProfile.UserId);
            return View(userProfile);
        }

        // GET: UserProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProfile = await _context.UserProfiles
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userProfile == null)
            {
                return NotFound();
            }

            return View(userProfile);
        }

        // POST: UserProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userProfile = await _context.UserProfiles.FindAsync(id);
            if (userProfile != null)
            {
                _context.UserProfiles.Remove(userProfile);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserProfileExists(int id)
        {
            return _context.UserProfiles.Any(e => e.Id == id);
        }
    }
}
