using FinancesMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FinancesMVC.Controllers
{
    public class InitialController : AuthorizeController
    {
        private static readonly decimal[] _limits = { 10000, 10000, 10000, 10000 };
        private static readonly decimal[] _expenditures = {
            _limits[0] * 0,
            _limits[1] * (decimal)0.25,
            _limits[2] * (decimal)0.75,
            _limits[3] * 1
        };

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateInitial()
        {
            //Food Category with no expenditures
            Category category = new()
            {

            };
            return RedirectToAction("Index", "Categories");
        }
    }
}
