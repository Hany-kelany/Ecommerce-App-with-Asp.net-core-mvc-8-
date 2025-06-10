
using Ecommerce.DataAccess.Data;
using Ecommerce.Entities.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers;

public class CategoryController : Controller
{

    private readonly ApplicationDbContext _context;

    public CategoryController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var categories = _context.categories.ToList();
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
            _context.categories.Add(category);
            _context.SaveChanges();
            TempData["Created"] = "Item Added Successfully";
            return RedirectToAction("Index");
        }
        return View(category);
    }
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var categoryindb = _context.categories.Find(id);
        if (categoryindb == null)
        {
            NotFound();
        }
        return View(categoryindb);
    }

    [HttpPost]
    public IActionResult Edit(Category category)
    {
        if (ModelState.IsValid)
        {
            _context.categories.Update(category);
            _context.SaveChanges();
            TempData["Updated"] = "Item Updated Successfully";
            return RedirectToAction("Index");
        }
        return View(category);
    }


    [HttpGet]
    public IActionResult Delete(int id)
    {
        var categoryindb = _context.categories.Find(id);
        if (categoryindb == null)
            return NotFound();
        return View(categoryindb);
    }


    [HttpPost]
    public IActionResult Deleteby(int id)
    {

        var categoryindb = _context.categories.Find(id);
        if (categoryindb == null)
            return NotFound();

        _context.categories.Remove(categoryindb);
        _context.SaveChanges();
        TempData["Deleted"] = "Item Deleted Successfully";
        return RedirectToAction("Index");
    }
}
