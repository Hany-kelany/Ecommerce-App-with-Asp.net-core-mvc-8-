using Ecommerce.DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Utilities;

namespace Ecommerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = Roles.AdminRole)]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext context;

        public UserController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var user = (ClaimsIdentity)User.Identity;
            var claim = user.FindFirst(ClaimTypes.NameIdentifier);
            var userid = claim.Value;
            var users = context.Users.Where(u => u.Id != userid).ToList();
            return View(users);
        }
    }
}
