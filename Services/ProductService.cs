using System;
using System.Transactions;
using ecom.DAO;
using ecom.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace ecom.Services;


public class ProductService : IProductService{
    private readonly ApplicationDbContext _db;

    public ProductService(ApplicationDbContext db){
        _db = db;
    }

    public async Task Create(CreateProductDto Dto){
        
    }
}