using ForApplication.DataAccess;
using ForApplication.Models;
using ForApplication.Models.Exceptions;
using System;

namespace ForApplication.BusinecLogic.Services;

public class ProductServic : IProductServic
{
    private readonly IProductDataAccess _dataAccess;
    public ProductServic(IProductDataAccess _dataAccess)
    {
        this._dataAccess = _dataAccess;
    }
    public async Task<bool> AddProductAsync(Product product)
    {
        var stored = await this._dataAccess.SelectProductByIdAsync(product.Id);

        if (stored is not null)
        {
            throw new AlreadyExistException($"{product.Id} bunday Idga ega mahsulot oldindan mavjud. Davom etish uchun biror tugmani bosing...");
        }

        await this._dataAccess.InsertProductAsync(product);

        return true;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var stored = await this._dataAccess.SelectProductByIdAsync(id);

        if (stored is null)
        {
            throw new NotFoundException($"{id} bunday Idga ega mahsulot topilmadi. Davom etish uchun biror tugmani bosing...");
        }

        await this._dataAccess.DeleteProductAsync(stored);

        return true;
    }

    public IQueryable<Product> GetAllProducts()
        => this._dataAccess.SelectAllProducts();

    public async Task<Product> SelectProductById(int id)
    {
        var stored = await this._dataAccess.SelectProductByIdAsync(id);

        if(stored is null)
        {
            throw new NotFoundException($"{id} bunday Idga ega mahsulot topilmadi. Davom etish uchun biror tugmani bosing...");
        }

        return stored;
    }

    public async Task<bool> UpdateProductAsync(Product product)
    {
        var stored = await this._dataAccess.SelectProductByIdAsync(product.Id);

        if(stored is null)
        {
            throw new NotFoundException($"{product.Id} bunday Idga ega mahsulot topilmadi. Davom etish uchun biror tugmani bosing...");
        }

        stored.Name = product.Name ?? stored.Name;
        stored.Category = product.Category ?? stored.Category;
        stored.Price = product.Price ?? stored.Price;
        stored.StockQuantity = stored.StockQuantity ?? stored.StockQuantity;

        await this._dataAccess.UpdateProductAsync(stored);

        return true;
    }
}