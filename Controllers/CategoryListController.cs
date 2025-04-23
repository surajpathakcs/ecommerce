using ecom.DAO;
using ecom.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ecom.Controllers
{
    public class CategoryListController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryListController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult CategoryDetail(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var category = _db.Category.FirstOrDefault(c => c.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            var products = _db.ProductItem.Where(p => p.CategoryId == id).ToList();

            var viewModel = new CategoryListVM
            {
                CategoryInfo = category,
                ProductItems = products
            };

            return View(viewModel);
        }
    }
}


