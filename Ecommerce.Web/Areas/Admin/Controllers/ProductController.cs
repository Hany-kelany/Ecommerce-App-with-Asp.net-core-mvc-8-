using Ecommerce.DataAccess.Repository;
using Ecommerce.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.Web.Areas.Admin.Controllers;
[Area("Admin")]
public class ProductController : Controller
{
    private readonly IGenericRepository<Product> repo;
    private readonly IGenericRepository<Category> categoryRepo;
    private readonly IWebHostEnvironment webHostEnvironment;

    public ProductController(IGenericRepository<Product> repo , IGenericRepository<Category> categoryRepo , IWebHostEnvironment webHostEnvironment)
    {
        this.repo = repo;
        this.categoryRepo = categoryRepo;
        this.webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
       var products = repo.GetAll();
        return View(products);
    }

    public IActionResult GetData()
    {
        var prods = repo.GetAll();
        return Json(new {data =  prods});   
    }
    [HttpGet]
    public IActionResult Create()
    {
        var categories = categoryRepo.GetAll();
        ViewBag.categories = new SelectList(categories, "Id", "Name");
        return View();
    }
    [HttpPost]
    public IActionResult Create(Product product , IFormFile ImageUpload)
    {
        if (ModelState.IsValid)
        {
            var filename = Guid.NewGuid().ToString();
            var rootpath = webHostEnvironment.WebRootPath;
            var ImagesPath = Path.Combine(rootpath, @"Images/Products");
            var ext = Path.GetExtension(ImageUpload.FileName);
            using (var filestr = new FileStream(Path.Combine(ImagesPath, filename +ext), FileMode.Create))
            {
                ImageUpload.CopyTo(filestr);    
            }
            product.Image= filename+ext;
            repo.Add(product);
            TempData["Created"] = "Item Added Successfully";
            return RedirectToAction("Index");
        }
        return View(product);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var categories = categoryRepo.GetAll();
        ViewBag.categories = new SelectList(categories, "Id", "Name");
        var productindb = repo.FindbyId(id);
        return View(productindb);
    }

    [HttpPost]
    public IActionResult Edit(Product product)
    {
        if (ModelState.IsValid)
        {
            var cat = repo.Edit(product, product.Id);
            TempData["Updated"] = "Item Updated Successfully";
            return RedirectToAction("Index");
        }
        return View(product);
    }

    //[HttpGet]
    //public IActionResult Delete(int id)
    //{
    //    var product = repo.FindbyId(id);
    //    return View(product);
    //}

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var pro =repo.Delete(id);
        var oldimage = Path.Combine(webHostEnvironment.WebRootPath, pro.Image);
        if (System.IO.File.Exists(oldimage)) 
        {
            System.IO.File.Delete(oldimage);
        }
        TempData["Deleted"] = "Item Deleted Successfully";
        return Json(new {success=true , data="Deleted Successfully..." });
        //return RedirectToAction("Index");
    }
}
