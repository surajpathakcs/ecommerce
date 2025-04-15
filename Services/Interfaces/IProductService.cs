using ecom.Dto.ProductDtos;
namespace ecom.Services.Interfaces;

public interface IProductService{
    /*Task<IEnumerable<ProductItemDto> GetAll();*/
    Task Create (CreateProductDto dto);
    Task Update (UpdateProductDto dto);
}