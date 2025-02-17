using ecom.DAO;
using ecom.Models;
using Microsoft.AspNetCore.Mvc;

namespace ecom.Controllers
{
    public class ProductItemController : Controller
    {
        public ApplicationDbContext _db;
        public ProductItemController(ApplicationDbContext db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            var productItems = _db.ProductItem.ToList();

            ProductItem productItem = new ProductItem();
            productItem.ProductItemName = "Laptop";
            productItem.ProductItemCode = "LT";
            productItem.CategoryId = 1;
            productItem.Description = "This is a laptop";
            productItem.UnitPrice = 1000;
            productItem.Thumbnail = "laptop.jpg";


            _db.Add(productItems);
            _db.SaveChanges();


            return View();
        }
    }
}
