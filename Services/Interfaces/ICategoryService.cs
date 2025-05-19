using ecom.Dto.Category;
using Microsoft.AspNetCore.Mvc;

namespace ecom.Services.Interfaces
{
    public interface ICategoryService
    {
        //list the services 
        Task<List<CategoryDto>> GetCategory();
        Task<JsonResult> CreateCategory(CreateCategoryDto createcategorydto);
        Task<Object> Edit(int id);
    }
}
