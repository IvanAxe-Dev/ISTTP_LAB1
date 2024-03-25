using FinancesMVC.ViewModels;
using FinancesMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinancesMVC.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AnonymousOnly]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AnonymousOnly]
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UsernameOrEmail);
                //login
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Categories");
                    }
                    ModelState.AddModelError("", "Invalid login data.");
                }
                
                return View(model);
            }
            return View(model);
        }

        [AnonymousOnly]
        [HttpGet]
        public IActionResult Register()
        {
            var yearList = Enumerable.Range(1900, DateTime.Now.Year - 1900 + 1)
                             .Select(y => new SelectListItem { Value = y.ToString(), Text = y.ToString() })
                             .Reverse()
                             .ToList();

            ViewBag.YearList = yearList; //error
            return View();
        }

        [AnonymousOnly]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new()
                {
                    UserName = model.Username,
                    Email = model.Email,
                    BirthYear = model.BirthYear,
                    IsMature = (DateTime.Now.Year - model.BirthYear) >= 18
                };

                var result = await _userManager.CreateAsync(user, model.Password!);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);

                    return RedirectToAction("Index", "Categories");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            var yearList = Enumerable.Range(1900, DateTime.Now.Year - 1900 + 1)
                             .Select(y => new SelectListItem { Value = y.ToString(), Text = y.ToString() })
                             .Reverse()
                             .ToList();

            ViewBag.YearList = yearList; //error
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }

}

public class AnonymousOnlyAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.User.Identity.IsAuthenticated)
        {
            // Redirect to a different page if already logged in
            context.Result = new RedirectToRouteResult(new RouteValueDictionary(
                new { controller = "Categories", action = "Index" }));
        }
    }
}