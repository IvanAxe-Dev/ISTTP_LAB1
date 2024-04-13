using FinancesMVC.ViewModels;
using FinancesMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace FinancesMVC.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
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
                var emailRegex = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

                var user = Regex.IsMatch(model.UsernameOrEmail, emailRegex) ?
                    await _userManager.FindByEmailAsync(model.UsernameOrEmail) :
                    await _userManager.FindByNameAsync(model.UsernameOrEmail);

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
                else
                {
                    ModelState.AddModelError("UsernameOrEmail", "User not found.");
                }

                return View(model);
            }
            return View(model);
        }

        [AnonymousOnly]
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var emailRegex = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

                var user = Regex.IsMatch(model.UsernameOrEmail, emailRegex) ?
                    await _userManager.FindByEmailAsync(model.UsernameOrEmail) :
                    await _userManager.FindByNameAsync(model.UsernameOrEmail);

                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { UserId = user.Id, code = code }, protocol: Request.Scheme);
                await _emailSender.SendEmailAsync(user.Email, "Reset Password", "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");
                return View("ForgotPasswordConfirmation");
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }


        //public async Task<ActionResult> ConfirmEmail(string userId, string code)
        //{
        //    if (userId == null || code == null)
        //    {
        //        return View("Error");
        //    }
        //    var result = await _userManager.ConfirmEmailAsync(userId, code);
        //    if (result.Succeeded)
        //    {
        //        return View("ConfirmEmail");
        //    }
        //    AddErrors(result);
        //    return View();
        //}

        [AnonymousOnly]
        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

        [AnonymousOnly]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(LoginViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    var emailRegex = @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            //    var user = Regex.IsMatch(model.UsernameOrEmail, emailRegex) ?
            //        await _userManager.FindByEmailAsync(model.UsernameOrEmail) :
            //        await _userManager.FindByNameAsync(model.UsernameOrEmail);

            //    //login
            //    if (user != null)
            //    {
            //        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code });
            //        await _userManager.SendEmailAsync(user, "Confirm Password Reset", "Please confirm your account by clicking this link: <a href =\"" + callbackUrl + "\">link</a>");
            //        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);
            //        if (result.Succeeded)
            //        {
            //            return RedirectToAction("Index", "Categories");
            //        }
            //        ModelState.AddModelError("", "Invalid login data.");
            //    }

            //    return View(model);
            //}
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
                    IsMature = (DateTime.Now.Year - model.BirthYear) >= 18,
                    UserProfile = new UserProfile(),
                    Categories = [
                        new()
                        {
                            Name = "Food",
                            ExpenditureLimit = 10000,
                            IsParentControl = false
                        },
                        new()
                        {
                            Name = "Entertainments",
                            ExpenditureLimit = 10000,
                            IsParentControl = false,
                            Transactions = [new()
                            {
                                MoneySpent = 5000,
                                Date = DateTime.UtcNow,
                                ExpenditureNote = "For entertainments"
                            }]
                        },
                        new()
                        {
                            Name = "Hobbies",
                            ExpenditureLimit = 10000,
                            IsParentControl = false,
                            Transactions = [new()
                            {
                                MoneySpent = 7500,
                                Date = DateTime.UtcNow,
                                ExpenditureNote = "For hobbies"
                            }]
                        },
                        new()
                        {
                            Name = "Clothes",
                            ExpenditureLimit = 10000,
                            IsParentControl = false,
                            Transactions = [new()
                            {
                                MoneySpent = 10000,
                                Date = DateTime.UtcNow,
                                ExpenditureNote = "For lethermen"
                            }]
                        },
                    ]
                };

                var result = await _userManager.CreateAsync(user, model.Password!);

                var roleResult = user.IsMature ?
                    await _userManager.AddToRoleAsync(user, "adult") :
                    await _userManager.AddToRoleAsync(user, "child");

                if (result.Succeeded && roleResult.Succeeded)
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