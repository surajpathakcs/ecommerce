using ecom.DAO;
using ecom.Models;
using ecom.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ecom.Controllers
{
    public class HomeController : Controller
    {
        public ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            // Check if an admin is logged in
            var adminId = HttpContext.Session.GetString("ADMIN_ID");


            DashboardVM dashboardvm = new DashboardVM();
            dashboardvm.CategoryInfo = _db.Category.ToList();
            dashboardvm.ProductItems = _db.ProductItem.ToList();

            ViewData["AdminLoggedIn"] = adminId != null;
            return View(dashboardvm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
