using ecom.Dto.ProductDtos;
using ecom.Dto.ProductItemDtos;
namespace ecom.Services.Interfaces;

public interface IProductService{
    Task<List<ProductItemDto>> GetAll();
    Task Create (CreateProductDto dto);
    Task Update (UpdateProductDto dto);
    Task <bool> Delete(int id);
}