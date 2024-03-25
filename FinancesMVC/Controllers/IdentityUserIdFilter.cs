using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace FinancesMVC.Controllers
{
    public class IdentityUserIdFilter : IActionFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityUserIdFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim != null)
            {
                context.ActionArguments["IdentityUserId"] = Guid.Parse(userIdClaim);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
