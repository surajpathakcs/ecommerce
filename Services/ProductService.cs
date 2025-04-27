using System;
using System.Transactions;
using ecom.DAO;
using System.Threading.Tasks;
using ecom.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace ecom.Services;

using ecom.Dto.ProductDtos;
using ecom.Dto.ProductItemDtos;
using ecom.Models;
using ecom.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _db;

    public ProductService(ApplicationDbContext db)
    {
        _db = db;
    }
    public async Task<List<ProductItemDto>> GetAll()
    {
        return await _db.ProductItem
            .Select(x => new ProductItemDto
            {
                ProductItemId = x.ProductItemId,
                ProductItemName = x.ProductItemName,
                ProductItemCode = x.ProductItemCode,
                CategoryId = x.CategoryId,
                Description = x.Description,
                UnitPrice = x.UnitPrice,
                Thumbnail = x.Thumbnail
            }).ToListAsync();
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
    public async Task<ProductItemDto> GetById(int id)
    {
        var product = await _db.ProductItem.FindAsync(id);
        if (product == null) return null;

        return new ProductItemDto
        {
            ProductItemId = product.ProductItemId,
            ProductItemName = product.ProductItemName,
            ProductItemCode = product.ProductItemCode,
            CategoryId = product.CategoryId,
            Description = product.Description,
            UnitPrice = product.UnitPrice,
            Thumbnail = product.Thumbnail
        };
    }


    public async Task<bool> Delete(int id)
    {
        var product = await _db.ProductItem.FindAsync(id);
        if(product == null)
        {
            return false;
        }
        _db.ProductItem.Remove(product);
        await _db.SaveChangesAsync();
        return true;
    }
}

