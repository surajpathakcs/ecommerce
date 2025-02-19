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
            return View();
        }
        
    }
}
