using AspNetCore;
using ecom.DAO;
using ecom.Dto.Category;
using ecom.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System;

namespace ecom.Services
{
    public class CategoryService
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        //implement the services
        public async Task<List<CategoryDto>> GetCategory()
        {
            return _dbContext.Category.Select(x => new CategoryDto
            {
                CategoryId = x.CategoryId,
                CategoryName = x.CategoryName,
                CategoryCode = x.CategoryCode,
                CreatedAt = x.CreatedAt.ToString("yyyy-MM-dd")
            }).ToList();
        }
        public async Task<Object> CreateCategory(CreateCategoryDto createcategorydto)
        {
            if (createcategorydto.hiddenId == 0)
            {
                var category = new Category();
                category.CategoryName = createcategorydto.CategoryName;
                category.CategoryCode = createcategorydto.CategoryCode;
                category.CreatedAt = System.DateTime.Now;

                _dbContext.Add(category);
                await _dbContext.SaveChangesAsync();

                return new
                {
                    success = true,
                    message = "Successfully Created Category"
                };

            }
            else
            {
                var category = _dbContext.Category.Where(x => x.CategoryId == createcategorydto.hiddenId).FirstOrDefault();

                if (category == null)
                {
                    return new
                    {
                        success = false,
                        message = "Category not found"
                    };
                }

                category.CategoryName = createcategorydto.CategoryName;
                category.CategoryCode = createcategorydto.CategoryCode;


                _dbContext.Update(category);
                await _dbContext.SaveChangesAsync();

                return new
                {
                    success = true,
                    message = "Successfully Updated Category"
                };
            }
        }
    
        public async Task<Object> Edit(int id)
        {
            var dbData = _dbContext.Category.Where(x => x.CategoryId == id).FirstOrDefault();

            if (dbData == null)
            {
                return new
                {
                    Success = false,
                    Message = "Category not found"
                };
            }
            else
            {
                return new
                {
                    Success = true,
                    Data = dbData
                };
            }
        }
    }
}
