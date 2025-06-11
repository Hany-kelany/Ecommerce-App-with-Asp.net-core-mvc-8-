using Ecommerce.DataAccess.Data;
using Ecommerce.DataAccess.Repository;
using Ecommerce.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Areas.Admin.Controllers;
[Area("Admin")]
public class CategoryController : Controller
{

    private readonly ApplicationDbContext _context;
    private readonly IGenericRepository<Category> repo;

    public CategoryController(ApplicationDbContext context, IGenericRepository<Category> repo)
    {
        _context = context;
        this.repo = repo;
    }

    public IActionResult Index()
    {
        var categories = repo.GetAll();
        return View(categories);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Category category)
    {
        if (ModelState.IsValid)
        {
            //_context.categories.Add(category);
            repo.Add(category);
            //_context.SaveChanges();
            TempData["Created"] = "Item Added Successfully";
            return RedirectToAction("Index");
        }
        return View(category);
    }
    [HttpGet]
    public IActionResult Edit(int id)
    {
        //var categoryindb = _context.categories.Find(id);
        var categoryindb = repo.FindbyId(id);
        //if (categoryindb == null)
        //{
        //    NotFound();
        //}
        return View(categoryindb);
    }

    [HttpPost]
    public IActionResult Edit(Category category)
    {
        if (ModelState.IsValid)
        {
            //_context.categories.Update(category);
            //_context.SaveChanges();
            var cat = repo.Edit(category, category.Id);
            TempData["Updated"] = "Item Updated Successfully";
            return RedirectToAction("Index");
        }
        return View(category);
    }


    [HttpGet]
    public IActionResult Delete(int id)
    {
        //var categoryindb = _context.categories.Find(id);
        //if (categoryindb == null)
        //    return NotFound();
        var categ = repo.FindbyId(id);
        return View(categ);
    }


    [HttpPost]
    public IActionResult Deleteby(int id)
    {

        //var categoryindb = _context.categories.Find(id);
        //if (categoryindb == null)
        //    return NotFound();

        //_context.categories.Remove(categoryindb);
        //_context.SaveChanges();
        repo.Delete(id);
        TempData["Deleted"] = "Item Deleted Successfully";
        return RedirectToAction("Index");
    }
}
