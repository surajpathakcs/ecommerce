using ecom.DAO;
using ecom.Dto.Category;
using ecom.Models;
using ecom.Models.ViewModel;
using ecom.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ecom.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryservice;
        private ApplicationDbContext _db;
        public CategoryController(ICategoryService categoryservice)
        {
            _categoryservice = categoryservice;
        }

        public IActionResult Index()
        {
            // Only show category page if the user is an admin
            if (!IsAdmin)
            {
                return RedirectToAction("AdminAccess", "Admin");
            }
            return View();
        }

        
        
        public async Task<IActionResult> GetCategories()
        {

            var datas = await _categoryservice.GetCategory();

            return Json(new { data = datas });
        }

        public async Task Save(CreateCategoryDto createcategorydto)
        {
            await _categoryservice.CreateCategory(createcategorydto);

            
            
        }
        public async Task<Object> Edit(int id)
        {
            var datas = await _categoryservice.Edit(id);
            return new { data =  datas};
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







/*public IActionResult CategoryDetail(int? id)
        {
            //call a  service
            
            if (id == null)
            {
                return NotFound();
            }

            var category = _db.Category.Where(x => x.CategoryId == id).ToList();
            if (category == null)
            {
                return NotFound();
            }

            var productitem = _db.ProductItem.Where(x => x.CategoryId == id).ToList();

            var viewmodel = new DashboardVM
            {
                CategoryInfo = category,
                ProductItems = productitem,
            };

            return View(viewmodel);
        }*/



/*
 if (hiddenId == 0)
{
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




 */