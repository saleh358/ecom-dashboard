using ECom_wep_app.Models;
using ECom_wep_app.Repository.Abstract;

namespace ECom_wep_app.Repository.Services;

public class CustomerRepository : ICustomerRepoitory
{ 
    private readonly EComDBContext _context;
    public CustomerRepository(EComDBContext context)
    {
        _context = context;
    }

    public Customer AddCustomer(Customer customer)
    {
        if (customer == null)
        {
            throw new ArgumentNullException(nameof(customer), "Customer cannot be null");
        }
        _context.Customer.Add(customer);
        _context.SaveChanges();
        return customer;
    }

    public void DeleteCustomer(int id)
    {
        var customer = GetCustomerById(id);
        if (customer == null)
        {
            throw new KeyNotFoundException($"Customer with ID {id} not found.");
        }
         _context.Customer.Remove(customer);
         _context.SaveChanges();
          return;
    }

    public List<Customer> GetAllCustomers()
    {
        return _context.Customer.ToList() ;
    }
    public Customer GetCustomerById(int id)
    {
        return _context.Customer.FirstOrDefault(c => c.Id == id);
    }

    public Customer UpdateCustomer(Customer customer)
    {
        if (customer == null)
        {
            throw new ArgumentNullException(nameof(customer), "Customer cannot be null");
        }
        _context.Customer.Update(customer);
        _context.SaveChanges();
        return customer;

    }
}
