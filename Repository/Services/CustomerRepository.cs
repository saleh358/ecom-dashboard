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

    public List<Customer> GetAllCustomers()
    {
        return _context.Customer.ToList() ;
    }
    public Customer GetCustomerById(int id)
    {
        return _context.Customer.FirstOrDefault(c => c.Id == id);
    }
}
