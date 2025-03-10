using ecom.DAO;
using ecom.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ecom.Controllers
{
    public class AdminController : Controller
    {

        private readonly ApplicationDbContext _db;
        
        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult AdminAccess()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminAccess(LoginVM vm)
        {
            var admin = _db.Admins.FirstOrDefault(a => a.Username == vm.Username);

            if (admin == null || admin.Password != vm.Password)
            {
                ViewBag.Error = "Invalid Username or Password!";
                return View();
            }

            // Store Admin Session
            HttpContext.Session.SetString("ADMIN_ID", admin.AdminId.ToString());// Retrieve session data
            var adminId = HttpContext.Session.GetString("ADMIN_ID");

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Logout()
        {
            //clear the session
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
    }
}
