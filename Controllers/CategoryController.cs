using ecom.DAO;
using ecom.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ecom.Controllers
{
    public class CategoryController : Controller
    {
        public ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var datas = _db.Category.ToList();
            return View(datas);
        }
        public JsonResult GetCategories()
        {
            var datas = _db.Category.Select(x => new
            {
                categoryId = x.CategoryId,
                categoryName = x.CategoryName,
                categoryCode = x.CategoryCode,
                createdAt = x.CreatedAt.ToString("yyyy-MM-dd")
            }).ToList();

            return Json(new { data = datas });
        }

        public JsonResult Save(int hiddenId, string CategoryName, string CategoryCode)
        {
            if (hiddenId == 0) { 
                Category category = new Category();
                category.CategoryName = CategoryName;
                category.CategoryCode = CategoryCode;
                category.CreatedAt = System.DateTime.Now;

                _db.Add(category);
                _db.SaveChanges();

                return Json(new
                {
                    success = true,
                    message = "Category has been saved successfully"
                });
            }
            else
            {
                var dbData = _db.Category.Where(x => x.CategoryId == hiddenId).FirstOrDefault();
                if (dbData == null)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Category not found"
                    });
                }
                else
                {
                    dbData.CategoryName = CategoryName;
                    dbData.CategoryCode = CategoryCode;

                    _db.Update(dbData);
                    _db.SaveChanges();

                    return Json(new
                    {
                        success = true,
                        message = "Category has been updated successfully"
                    });
                }
            
        }
        }
        public JsonResult Edit(int id)
        {
            var dbData = _db.Category.Where(x => x.CategoryId == id).FirstOrDefault();

            if (dbData == null)
            {
                return Json(new
                {
                    Success = false,
                    Message = "Category not found"
                });
            }
            else
            {
                return Json(new
                {
                    Success = true,
                    Data = dbData
                });
            }
        }

        //Delete JsonResult

        public JsonResult Delete(int? id)
        {
            Category category = new Category();

            var dbData = _db.Category.Where(x => x.CategoryId == id).FirstOrDefault();

            if(dbData == null)
            {
                return Json(new
                {
                    Success = false,
                    Message = "Category not found"
                });
            }
            else
            {
                _db.Category.Remove(dbData);
                _db.SaveChanges();

                return Json(new
                {
                    Success = true,
                    Message = "Category deleted"
                });
            }

        }
    }
}

