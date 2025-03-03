using ecom.DAO;
using ecom.Models;
using Microsoft.AspNetCore.Mvc;

namespace ecom.Controllers
{
    public class ProductOrderController : Controller
    {
        private ApplicationDbContext _db;
        public ProductOrderController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<ProductOrderMasterVM> orders = _db.ProductOrderMaster.Select(s => new ProductOrderMasterVM
            {
                Address = s.Address,
                Email = s.Email,
                Fullname = s.Fullname,
                GrandTotal = s.GrandTotal,
                MobileNo = s.MobileNo,
                OrderDate = s.OrderDate,
                ProductOrderMasterID = s.ProductOrderMasterID,
                ItemCount = _db.ProductOrderMaster.Where(w => w.ProductOrderMasterID == s.ProductOrderMasterID).Count()
            }).ToList();
            return View(orders);
        }

        public IActionResult OrderDetails(int id)
        {
            var orders = _db.ProductOrderDetail.Where(x => x.ProductOrderMasterID == id)
                .Select(s => new ProductOrderDetailVM
            {
                ProductOrderDetailID = s.ProductOrderDetailID,
                ProductItemId = s.ProductItemId,
                ProductName = s.ProductItem.ProductItemName,//_db.ProductItem.Where(p => p.ProductItemId== s.ProductItemId).Select(p => p.ProductItemName).FirstOrDefault(),
                UnitPrice = s.UnitPrice,
                Quantity = s.Quantity,
                TotalPrice = s.UnitPrice * s.Quantity,
                ItemCount = _db.ProductOrderDetail.Count(w => w.ProductOrderMasterID == id)
            }).ToList();
            return View(orders);
        }
    }
}
