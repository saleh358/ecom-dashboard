using ECom_wep_app.Models;
using ECom_wep_app.Models.Search;
using ECom_wep_app.Repository.Abstract;
using ECom_wep_app.Service.Abstract;

namespace ECom_wep_app.Service.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task<List<Product>> GetAllProductsAsync()
    {
        return _productRepository.GetAllProductsAsync();
    }

    public Task<Product> GetProductByIdAsync(int id)
    {
        return _productRepository.GetProductByIdAsync(id);
    }

    public Task<Product> AddProductAsync(Product product)
    {
        return _productRepository.AddProductAsync(product);
    }

    public Task<Product> UpdateProductAsync(Product product)
    {
        return _productRepository.UpdateProductAsync(product);
    }

    public Task DeleteProductAsync(int id)
    {
        return _productRepository.DeleteProduct(id);
    }

    public Task<IQueryable<Product>> ProductSearchAsync(ProductSearchModel model)
    {
        return _productRepository.ProductSearchAsync(model);
    }
}
