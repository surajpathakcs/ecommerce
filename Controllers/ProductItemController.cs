using ecom.DAO;
using ecom.Dto.ProductDtos;
using System.Threading.Tasks;
using ecom.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ecom.Controllers
{
    public class ProductItemController : BaseController
    {
        /*        private ApplicationDbContext _db;*/
        /*  Instead use IProductService here  */

        private readonly IProductService _productservice;

        public ProductItemController(IProductService productservice)
        {
            _productservice = productservice;
        }


        


        public async Task<IActionResult> Index()
        {
            // Only show category page if the user is an admin
            if (!IsAdmin)
            {
                return RedirectToAction("AdminAccess", "Admin");
            }

            var datas =await _productservice.GetAll();
            return View(datas);
        }


        public async Task<JsonResult> GetProductItems()
        {
            var datas = await _productservice.GetAll();
            return Json(new { data = datas});
        }

        public async Task<IActionResult> Create([FromBody] CreateProductDto createproductdto)
        {
            await _productservice.Create(createproductdto);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update([FromBody] UpdateProductDto updateproductdto)
        {
            await _productservice.Update(updateproductdto);
            return RedirectToAction("Index");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest(new { message = "Invalid product ID" });
            }

            var success = await _productservice.Delete(id.Value);
            if (!success) return NotFound(new { message = "The Product Item was not found" });

            return Ok(new { message = "Product deleted successfully" });
        }


    }
}


























/*      
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
       
*/
/*
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
*/
/*       public JsonResult Delete(int? id)
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
*/

