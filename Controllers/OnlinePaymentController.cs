using Microsoft.AspNetCore.Mvc;

namespace ecom.Controllers
{
    public class OnlinePaymentController : Controller
    {
        [HttpPost]
        public IActionResult Index()
        {
            return View();
        }


    }
}
