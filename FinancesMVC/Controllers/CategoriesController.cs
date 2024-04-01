using FinancesMVC.Models;
using FinancesMVC.Services;
using FinancesMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FinancesMVC.Controllers
{
    public class CategoriesController : AuthorizeController
    {
        private readonly Db1Context _context;

        public CategoriesController(Db1Context context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var db1Context = _context.Categories
                .Include(c => c.User)
                .Include(c => c.Transactions)
                .Where(c => c.SharedBudgets.Count() == 0)
                .Where(c => c.UserId == IdentityUserId);
            return View(await db1Context.ToListAsync());
        }

        // GET: Transactions/Index
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.User)
                .Include(c => c.Transactions)
                .Where(c => c.UserId == IdentityUserId)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Transactions", new { id = category.Id, name = category.Name });
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
                .Include(c => c.Transactions)
                .Where(c => c.UserId == IdentityUserId)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            return RedirectToAction("Create", "Transactions", new { id = category.Id, name = category.Name });
        }

        // GET: Categories/Create
        public IActionResult Create(bool isShared)
        {
            if (isShared)
            {
                ViewBag.IsShared = true;
                ViewData["Users"] = _context.Users
                    .Where(u => u.Id != IdentityUserId)
                    .Select(u => u.UserName)
                    .ToList();
            }
            else
                ViewBag.IsShared = false;
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Category, SelectedUsers")] CategoryViewModel generalCategory)
        {

            ModelState.Clear();
            TryValidateModel(generalCategory.Category);

            if (ModelState.IsValid)
            {
                generalCategory.Category.UserId = IdentityUserId;
                _context.Add(generalCategory.Category);
                if (generalCategory.SelectedUsers != null)
                {
                    SharedBudget sharedBudget = new()
                    {
                        OwnerId = IdentityUserId,
                        CommonCategory = generalCategory.Category
                    };
                    sharedBudget.AddedUsersId = new List<Guid>();
                    var serializedArray = generalCategory.SelectedUsers.Count() > 1 ? JsonConvert.DeserializeObject<List<string>>(generalCategory.SelectedUsers[1])
                        : generalCategory.SelectedUsers;
                    foreach (var user in serializedArray)
                    {
                        User? userInContext = _context.Users.FirstOrDefault(u => u.UserName == user);
                        if (userInContext != null)
                        {
                            sharedBudget.AddedUsersId.Add(userInContext.Id);
                        }
                    }
                    _context.Add(sharedBudget);
                }
                await _context.SaveChangesAsync();
                return generalCategory.SelectedUsers == null ? RedirectToAction(nameof(Index)) : RedirectToAction("Index", "SharedBudgets");
            }

            return View(generalCategory);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            ViewBag.Name = category.Name;
            ViewData["UserId"] = IdentityUserId;
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,UserId,TotalExpences,ChosenGoalId,ExpenditureLimit,IsParentControl")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    category.UserId = IdentityUserId;
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            ViewData["UserId"] = IdentityUserId;
            return View(category);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }

        //  [HttpGet]
        //  public async Task<IActionResult> Export([FromQuery] string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //CancellationToken cancellationToken = default)
        //  {
        //      var exportService = _categoryDataPortServiceFactory.GetExportService(contentType);

        //      var memoryStream = new MemoryStream();

        //      await exportService.WriteToAsync(memoryStream, cancellationToken);

        //      await memoryStream.FlushAsync(cancellationToken);
        //      memoryStream.Position = 0;


        //      return new FileStreamResult(memoryStream, contentType)
        //      {
        //          FileDownloadName = $"categiries_{DateTime.UtcNow.ToShortDateString()}.xlsx"
        //      };
        //  }

    }
}
