using ecom.DAO;
using ecom.Models;
using ecom.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ecom.Controllers
{
    public class OnlinePaymentController : Controller
    {
        private ApplicationDbContext _db;
        public OnlinePaymentController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpPost]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Index(int orderId)
        {
            var orderMaster = _db.ProductOrderMaster.FirstOrDefault(o => o.ProductOrderMasterID == orderId);
            if (orderMaster == null)
            {
                return NotFound("Order not found.");
            }
            var orderDetails = _db.ProductOrderDetail
                                 .Where(d => d.ProductOrderMasterID == orderId)
                                 .Select(d => new ProductOrderDetailVM
                                 {
                                     ProductOrderDetailID = d.ProductOrderDetailID,
                                     ProductItemId = d.ProductItemId,
                                     ProductName = d.ProductItem.ProductItemName, // Get ProductName from ProductItem
                                     UnitPrice = d.UnitPrice,
                                     Quantity = d.Quantity,
                                     TotalPrice = d.UnitPrice * d.Quantity,
                                     ItemCount = d.Quantity
                                 })
                                 .ToList();

            var orderVM = new OrderVM
            {
                mast = orderMaster,
                dtl = orderDetails
            };

            return View(orderVM);
        }


    }
}
