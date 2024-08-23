using ForApplication.Models;

namespace ForApplication.BusinecLogic.Services;

public interface IProductServic
{
    Task<bool> AddProductAsync(Product product);
    Task<bool> UpdateProductAsync(Product product);
    Task<bool> DeleteProductAsync(int id);
    Task<Product> SelectProductById(int id);
    IQueryable<Product> GetAllProducts();
}