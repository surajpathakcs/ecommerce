using ecom.DAO;
using ecom.Models;
using Microsoft.AspNetCore.Mvc;

namespace ecom.Controllers
{
    public class CategoryController : Controller
    {
        public ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public JsonResult Save(string CategoryName, string CategoryCode)
        {
            Category category = new Category();
            category.CategoryName = CategoryName;
            category.CategoryCode = CategoryCode;
            category.CreatedAt = System.DateTime.Now;

            _db.Add(category);
            _db.SaveChanges();

            return Json(new {
                success = true,
                message = "Category has been saved successfully"
            });
        }
        public IActionResult Index()
        {
            var datas = _db.Category.ToList();


            /*Category category = new Category();

            category.CategoryName= "Electronics";
            category.CategoryCode = "EL";
            category.CreatedAt = System.DateTime.Now;

            _db.Add(category);
            _db.SaveChanges();*/




            return View(datas);
        }
    }
}
