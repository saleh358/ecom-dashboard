using ECom_wep_app.Models;
using ECom_wep_app.Models.Search;

namespace ECom_wep_app.Service.Abstract;

public interface ICustomerService
{
    Task<List<Customer>> GetAllCustomersAsync();
    Task<Customer> GetCustomerByIdAsync(int id);
    Task<Customer> AddCustomerAsync(Customer customer);
    Task<Customer> UpdateCustomerAsync(Customer customer);
    Task DeleteCustomerAsync(int id);
    Task<IQueryable<Customer>> CustomerSearchAsync(CustomerSearchModel model);
}
