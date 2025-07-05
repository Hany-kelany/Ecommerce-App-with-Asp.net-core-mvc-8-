using Ecommerce.DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Utilities;

namespace Ecommerce.Web.Areas.Admin.Controllers;

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
    public IActionResult LockUnlock(string id)
    {
        var user = context.Users.FirstOrDefault(i => i.Id == id);
        if (user == null)
            return NotFound();
        if(user.LockoutEnd==null || user.LockoutEnd<DateTime.Now)
        {
            user.LockoutEnd= DateTime.Now.AddYears(1);
        }
        else
        {
            user.LockoutEnd = DateTime.Now;
        }
        context.SaveChanges();
        return RedirectToAction("index", "User", new { area = "admin" });
    }

    public IActionResult DeleteUser(string id)
    {
        var user = context.Users.FirstOrDefault(i => i.Id == id);
        if (user == null)
            TempData["ErrorMessage"] = "المستخدم غير موجود.";

        context.Users.Remove(user);
   
        TempData["SuccessMessage"] = "Deleted Sussfully...";

        context.SaveChanges();
        return RedirectToAction("Index", "User", new { area = "Admin" });
          
    }

}
