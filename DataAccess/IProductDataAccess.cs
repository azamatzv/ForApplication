using ForApplication.Models;

namespace ForApplication.DataAccess;

public interface IProductDataAccess
{
    Task InsertProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(Product product);
    Task<Product?> SelectProductByIdAsync(int id);
    IQueryable<Product> SelectAllProducts();
}