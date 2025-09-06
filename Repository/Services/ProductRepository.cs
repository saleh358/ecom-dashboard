using ECom_wep_app.Models;
using ECom_wep_app.Models.Utilities;
using ECom_wep_app.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECom_wep_app.Repository.Services;

public class ProductRepository : IProductRepository
{
    private readonly EComDBContext _context;
    public ProductRepository(EComDBContext context)
    {
        _context = context;
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product), "Product cannot be null");
        }
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task DeleteProduct(int id)
    {
        var product = await GetProductByIdAsync(id);
        if (product == null)
        {
            throw new KeyNotFoundException($"Product with ID {id} not found.");
        }
        _context.Products.Remove(product);
        _context.SaveChanges();
        return;
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }
    public async Task<Product> GetProductByIdAsync(int id)
    {
        return _context.Products.FirstOrDefault(c => c.Id == id);
    }

    public async Task<Product> UpdateProductAsync(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product), "Product cannot be null");
        }
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;

    }
    public async Task<PaginatedList<Product>> GetProductsAsync(int pageIndex, int pageSize, string searchTerm = null)
    {
        pageIndex = pageIndex < 1 ? 1 : pageIndex;
        pageSize = pageSize <= 0 ? 10 : pageSize;
        searchTerm = string.IsNullOrWhiteSpace(searchTerm) ? null : searchTerm.Trim();

        var query = _context.Products.AsNoTracking().AsQueryable();

        if (searchTerm != null)
        {
            var pattern = $"%{searchTerm}%";
            query = query.Where(c =>
                EF.Functions.Like(c.Name, pattern) 
            );
        }

        query = query.OrderBy(c => c.Id);

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedList<Product>(items, pageIndex, pageSize, totalCount);
    }


}
