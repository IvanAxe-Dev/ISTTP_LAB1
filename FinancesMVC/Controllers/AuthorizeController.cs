using FinancesMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinancesMVC.Controllers
{
    [Authorize]
    public class AuthorizeController : Controller
    {
        protected Guid IdentityUserId
        {
            get { return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)); }
        }
    }
}
