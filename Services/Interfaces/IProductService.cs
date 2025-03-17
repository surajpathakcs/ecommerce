using System;
namespace ecom.Services.Interfaces;

public interface IProductService{
    Task Create (CreateProductDto dto);
    Task Update (UpdateProductDto dto);
}