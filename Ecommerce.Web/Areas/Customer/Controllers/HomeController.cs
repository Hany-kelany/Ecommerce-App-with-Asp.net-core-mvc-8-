using Ecommerce.DataAccess.Repository;
using Ecommerce.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Areas.Customer.Controllers;
[Area("Customer")]
public class HomeController : Controller
{
    private readonly IGenericRepository<Product> repo;

    public HomeController(IGenericRepository<Product> repo)
    {
        this.repo = repo;
    }
    public IActionResult Index()
    {
        var products = repo.GetAll();   
        return View(products);
    }
}
