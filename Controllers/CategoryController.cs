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
