using ecom.Models.ViewModel;
using ecom.Models;
using Microsoft.AspNetCore.Mvc;
using ecom.DAO;

namespace ecom.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext _db;

        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index(int? id)
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveOrder([FromBody] OrderVM vm)
        {
            if (vm == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Invalid order data"
                });
            }
            if (string.IsNullOrEmpty(vm.mast.Fullname))
            {
                return Json(new
                {
                    success = false,
                    message = "Enter Full Name"
                });

            }
            else if (string.IsNullOrEmpty(vm.mast.Email))
            {
                return Json(new
                {
                    success = false,
                    message = "Enter Email"
                });
            }
            else if (string.IsNullOrEmpty(vm.mast.MobileNo))
            {
                return Json(new
                {
                    success = false,
                    message = "Enter Mobile Number"
                });
            }
            else if (string.IsNullOrEmpty(vm.mast.Address))
            {
                return Json(new
                {
                    success = false,
                    message = "Enter Your Address"
                });
            }
            else if (vm.detail.Count == 0)
            {
                return Json(new
                {
                    Success = false,
                    Message = "Item Not Found"
                });
            }
            else
            {
                //first save in master
                ProductOrderMaster m = new ProductOrderMaster()
                {
                    Address = vm.mast.Address,
                    Email = vm.mast.Email,
                    Fullname = vm.mast.Fullname,
                    MobileNo = vm.mast.MobileNo,
                    GrandTotal = vm.mast.GrandTotal,
                    OrderDate = DateTime.Now,
                    PaymentStatus = "Pending",
                    TransactionId = ""
                };

                _db.ProductOrderMaster.Add(m);
                _db.SaveChanges();


                //Now save detail

                List<ProductOrderDetail> d = new List<ProductOrderDetail>();
                foreach (var item in vm.detail)
                {
                    d.Add(new ProductOrderDetail
                    {
                        ProductItemId = item.Id,
                        UnitPrice = item.UnitPrice,
                        Quantity = item.Quantity,
                        ProductOrderMasterID = m.ProductOrderMasterID
                    });
                }
                _db.ProductOrderDetail.AddRange(d);
                _db.SaveChanges();
                return Json(new
                {
                    success = true,
                    message = "User Detail Saved and Order Placed Successfully",
                    data = d
                });

            }
        }


    }
}
