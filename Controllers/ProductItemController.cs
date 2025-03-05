using ecom.DAO;
using ecom.Models;
using ecom.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ecom.Controllers
{
    public class ProductItemController : Controller
    {
        private ApplicationDbContext _db;

        public ProductItemController(ApplicationDbContext db)
        {
            _db = db;
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
            else{
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



        public IActionResult Cart(int? id)
        {
            return View();
        }

        public IActionResult Index()
        {
            var datas = _db.ProductItem.ToList();
            return View(datas);
        }


        public JsonResult GetProductItems()
        {
            var datas = _db.ProductItem.Select(x => new
            {
                productItemId = x.ProductItemId,
                productItemName = x.ProductItemName,
                productItemCode = x.ProductItemCode,
                categoryId = x.CategoryId,
                description = x.Description,
                unitPrice = x.UnitPrice,
                thumbnail = x.Thumbnail
            }).ToList();

            return Json(new { data = datas });
        }

        public JsonResult Save(int hiddenId, string ProductItemName, string ProductItemCode, int CategoryId, string Description, decimal UnitPrice, string Thumbnail)
        {
            if (hiddenId == 0)
            {
                ProductItem productItem = new ProductItem();
                productItem.ProductItemName = ProductItemName;
                productItem.ProductItemCode = ProductItemCode;
                productItem.CategoryId = CategoryId;
                productItem.Description = Description;
                productItem.UnitPrice = UnitPrice;
                productItem.Thumbnail = Thumbnail;

                _db.Add(productItem);
                _db.SaveChanges();

                return Json(new
                {
                    success = true,
                    message = "Product item has been saved successfully"
                });
            }
            else
            {
                var dbData = _db.ProductItem.Where(x => x.ProductItemId == hiddenId).FirstOrDefault();
                if (dbData == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Product item not found"
                    });
                }
                else
                {
                    dbData.ProductItemName = ProductItemName;
                    dbData.ProductItemCode = ProductItemCode;
                    dbData.CategoryId = CategoryId;
                    dbData.Description = Description;
                    dbData.UnitPrice = UnitPrice;
                    dbData.Thumbnail = Thumbnail;

                    _db.Update(dbData);
                    _db.SaveChanges();

                    return Json(new
                    {
                        success = true,
                        message = "Product item has been updated successfully"
                    });
                }
            }
        }

        public JsonResult Edit(int id)
        {
            var dbData = _db.ProductItem.Where(x => x.ProductItemId == id).FirstOrDefault();

            if (dbData == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Product item not found"
                });
            }
            else
            {
                return Json(new
                {
                    success = true,
                    data = dbData
                });
            }
        }

        public JsonResult Delete(int? id)
        {
            var dbData = _db.ProductItem.FirstOrDefault(x => x.ProductItemId == id);

            if (dbData == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Product item not found"
                });
            }
            else
            {
                _db.ProductItem.Remove(dbData);
                _db.SaveChanges();

                return Json(new
                {
                    success = true,
                    message = "Product item deleted successfully"
                });
            }
        }


    }
}
