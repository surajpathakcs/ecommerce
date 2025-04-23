using ecom.DAO;
using ecom.Models;
using ecom.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ecom.Controllers
{
    public class ProductItemController : BaseController
    {
        private ApplicationDbContext _db;

        public ProductItemController(ApplicationDbContext db)
        {
            _db = db;
        }


        


        public IActionResult Index()
        {
            // Only show category page if the user is an admin
            if (!IsAdmin)
            {
                return RedirectToAction("AdminAccess", "Admin");
            }

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
