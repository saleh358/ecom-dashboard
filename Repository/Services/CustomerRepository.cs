using ECom_wep_app.Models;
using ECom_wep_app.Models.Utilities;
using ECom_wep_app.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECom_wep_app.Repository.Services;

public class CustomerRepository : ICustomerRepoitory
{ 
    private readonly EComDBContext _context;
    public CustomerRepository(EComDBContext context)
    {
        _context = context;
    }

    public async Task<Customer> AddCustomerAsync(Customer customer)
    {
        if (customer == null)
        {
            throw new ArgumentNullException(nameof(customer), "Customer cannot be null");
        }
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();
        return customer;
    }

    public async Task DeleteCustomer(int id)
    {
        var customer =await GetCustomerByIdAsync(id);
        if (customer == null)
        {
            throw new KeyNotFoundException($"Customer with ID {id} not found.");
        }
         _context.Customers.Remove(customer);
         _context.SaveChanges();
          return;
    }

    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        return await _context.Customers.ToListAsync();
    }
    public async Task<Customer> GetCustomerByIdAsync(int id)
    {
        return _context.Customers.FirstOrDefault(c => c.Id == id);
    }

    public async Task<Customer> UpdateCustomerAsync(Customer customer)
    {
        if (customer == null)
        {
            throw new ArgumentNullException(nameof(customer), "Customer cannot be null");
        }
       _context.Customers.Update(customer);
       await _context.SaveChangesAsync();
        return customer;

    }
    public async Task<PaginatedList<Customer>> GetCustomersAsync(int pageIndex, int pageSize, string searchTerm = null)
    {
        pageIndex = pageIndex < 1 ? 1 : pageIndex;
        pageSize = pageSize <= 0 ? 10 : pageSize;
        searchTerm = string.IsNullOrWhiteSpace(searchTerm) ? null : searchTerm.Trim();

        var query =  _context.Customers.AsNoTracking().AsQueryable();

        if (searchTerm != null)
        {
            var pattern = $"%{searchTerm}%";
            query = query.Where(c =>
                EF.Functions.Like(c.Name, pattern) ||
                EF.Functions.Like(c.Email, pattern)
            );
        }

        query = query.OrderBy(c => c.Id);

        var totalCount =await query.CountAsync();

        var items =await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedList<Customer>(items, pageIndex, pageSize, totalCount);
    }

  
}
