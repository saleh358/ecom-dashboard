using ECom_wep_app.Models;
using ECom_wep_app.Models.Search;

namespace ECom_wep_app.Service.Abstract;

public interface IProductService
{
    Task<List<Product>> GetAllProductsAsync();
    Task<Product> GetProductByIdAsync(int id);
    Task<Product> AddProductAsync(Product product);
    Task<Product> UpdateProductAsync(Product product);
    Task DeleteProductAsync(int id);
    Task<IQueryable<Product>> ProductSearchAsync(ProductSearchModel model);
}
