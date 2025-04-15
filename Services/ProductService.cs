using System;
using System.Transactions;
using ecom.DAO;
using ecom.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace ecom.Services;

using ecom.Dto.ProductDtos;
using ecom.Models;
using ecom.Services.Interfaces;
using System.Threading.Tasks;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _db;

    public ProductService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task Create(CreateProductDto dto)
    {
        var productItem = new ProductItem
        {
            ProductItemName = dto.ProductItemName,
            ProductItemCode = dto.ProductItemCode,
            CategoryId = dto.CategoryId,
            Description = dto.Description,
            UnitPrice = dto.UnitPrice,
            Thumbnail = dto.Thumbnail
        };

        _db.ProductItem.Add(productItem);
        await _db.SaveChangesAsync();
    }

    public async Task Update(UpdateProductDto dto)
    {
        var productItem = await _db.ProductItem.FindAsync(dto.ProductItemId);
        if (productItem == null) throw new Exception("Product not found");

        productItem.ProductItemName = dto.ProductItemName;
        productItem.ProductItemCode = dto.ProductItemCode;
        productItem.CategoryId = dto.CategoryId;
        productItem.Description = dto.Description;
        productItem.UnitPrice = dto.UnitPrice;
        productItem.Thumbnail = dto.Thumbnail;

        await _db.SaveChangesAsync();
    }
}

