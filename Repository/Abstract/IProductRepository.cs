using ECom_wep_app.Models;
using ECom_wep_app.Models.Search;
using ECom_wep_app.Models.Utilities;

namespace ECom_wep_app.Repository.Abstract;

public interface IProductRepository
{
    public Task<List<Product>> GetAllProductsAsync();
    public Task<Product> GetProductByIdAsync(int id);
    public Task<Product> AddProductAsync(Product Product);
    public Task<Product> UpdateProductAsync(Product Product);
    public Task DeleteProduct(int id);

    public Task<IQueryable<Product>> ProductSearchAsync(ProductSearchModel model);
}
