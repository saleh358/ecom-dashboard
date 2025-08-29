using ECom_wep_app.Models;
using ECom_wep_app.Models.Search;
using ECom_wep_app.Models.Utilities;
using ECom_wep_app.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        await _context.Customer.AddAsync(customer);
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
         _context.Customer.Remove(customer);
         _context.SaveChanges();
          return;
    }

    public async Task<List<Customer>> GetAllCustomersAsync()
    {
        return await _context.Customer.ToListAsync();
    }
    public async Task<Customer> GetCustomerByIdAsync(int id)
    {
        return _context.Customer.FirstOrDefault(c => c.Id == id);
    }

    public async Task<Customer> UpdateCustomerAsync(Customer customer)
    {
        if (customer == null)
        {
            throw new ArgumentNullException(nameof(customer), "Customer cannot be null");
        }
       _context.Customer.Update(customer);
       await _context.SaveChangesAsync();
        return customer;

    }
    public async Task<IQueryable<Customer>> CustomerSearchAsync(CustomerSearchModel model)
    {
        var result = _context.Customer.AsQueryable();

        if (model != null)
        {
            if (model.Id != null)
                result = result.Where(c => c.Id == model.Id);

            if (!string.IsNullOrWhiteSpace(model.Name))
            {
                var p = $"%{model.Name.Trim()}%";
                result = result.Where(c => EF.Functions.Like(c.Name, p));
            }
            if (!string.IsNullOrWhiteSpace(model.Email))
            {
                var p = $"%{model.Email.Trim()}%";
                result = result.Where(c => EF.Functions.Like(c.Email, p));
            }
            if (!string.IsNullOrWhiteSpace(model.PhoneNumber))
            {
                var p = $"%{model.PhoneNumber.Trim()}%";
                result = result.Where(c => EF.Functions.Like(c.PhoneNumber, p));
            }
            if (!string.IsNullOrWhiteSpace(model.Address))
            {
                var p = $"%{model.Address.Trim()}%";
                result = result.Where(c => EF.Functions.Like(c.Address, p));
            }
            if (!string.IsNullOrWhiteSpace(model.OrderBy))
            {
                switch (model.OrderBy.Trim().ToLower())
                {
                    case "id":
                        result = result.OrderBy(c => c.Id);
                        break;
                    case "name":
                        result = result.OrderBy(c => c.Name);
                        break;
                    case "email":
                        result = result.OrderBy(c => c.Email);
                        break;
                    case "phonenumber":
                        result = result.OrderBy(c => c.PhoneNumber);
                        break;
                    case "address":
                        result = result.OrderBy(c => c.Address);
                        break;
                    default:
                        result = result.OrderBy(c => c.Id);
                        break;
                }
            }
            else
            {
                result = result.OrderBy(c => c.Id);
            }
        }


        return await Task.FromResult(result);
    }


}
