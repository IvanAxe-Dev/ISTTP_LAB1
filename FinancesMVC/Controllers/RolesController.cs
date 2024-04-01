//using FinancesMVC.Models;
//using FinancesMVC.ViewModels;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace FinancesMVC.Controllers
//{

//    public class RolesController : Controller
//    {
//        RoleManager<IdentityRole<Guid>> _roleManager;
//        UserManager<User> _userManager;
//        public RolesController(RoleManager<IdentityRole<Guid>> roleManager, UserManager<User> userManager)
//        {
//            _roleManager = roleManager;
//            _userManager = userManager;
//        }
//        public IActionResult Index() => View(_roleManager.Roles.ToList());
//        public IActionResult UserList() => View(_userManager.Users.ToList());

//        [HttpGet]
//        public async Task<IActionResult> Edit(string userId)
//        {
//            // отримуємо користувача
//            User user = await _userManager.FindByIdAsync(userId);
//            if (user != null)
//            {
//                //список ролей користувача
//                var userRoles = await _userManager.GetRolesAsync(user);
//                var allRoles = _roleManager.Roles.ToList();
//                ChangeRoleViewModel model = new ChangeRoleViewModel
//                {
//                    UserId = user.Id,
//                    UserEmail = user.Email,
//                    UserRoles = userRoles,
//                    AllRoles = allRoles
//                };
//                return View(model);
//            }

//            return NotFound();
//        }

//        [HttpPost]
//        public async Task<IActionResult> Edit(Guid userId, List<string> roles)
//        {
//            // отримуємо користувача
//            User user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);

//            if (user != null)
//            {
//                // список ролей користувача
//                var userRoles = await _userManager.GetRolesAsync(user);
//                // получаем все роли
//                var allRoles = _roleManager.Roles.ToList();
//                // список ролей, які було додано
//                var addedRoles = roles.Except(userRoles);
//                // список ролей, які було видалено
//                var removedRoles = userRoles.Except(roles);

//                await _userManager.AddToRolesAsync(user, addedRoles);

//                await _userManager.RemoveFromRolesAsync(user, removedRoles);

//                return RedirectToAction("Index", "Categories");
//            }

//            return NotFound();
//        }

//    }
//}

