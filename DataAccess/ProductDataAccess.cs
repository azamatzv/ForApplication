using ForApplication.Data;
using ForApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace ForApplication.DataAccess;

public class ProductDataAccess : IProductDataAccess
{
    private readonly AppDbContext _context;
    public ProductDataAccess(AppDbContext db)
    {
        _context = db;
    }
    public async Task DeleteProductAsync(Product product)
    {
        var result = this._context.Products.Remove(product);
        await this._context.SaveChangesAsync();
    }

    public async Task InsertProductAsync(Product product)
    {
        var result = this._context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public IQueryable<Product> SelectAllProducts() 
        => this._context.Products.AsNoTracking();

    public async Task<Product?> SelectProductByIdAsync(int id)
    {
        return await this._context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
    }

    public async Task UpdateProductAsync(Product product)
    {
        var result = this._context.Products.Update(product);
        await _context.SaveChangesAsync();
    }
}