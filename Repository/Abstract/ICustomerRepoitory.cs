using ECom_wep_app.Models;
using ECom_wep_app.Models.Utilities;

namespace ECom_wep_app.Repository.Abstract;

public interface ICustomerRepoitory
{
    public Task<List<Customer>> GetAllCustomersAsync();
    public Task<Customer> GetCustomerByIdAsync(int id);
    public Task<Customer> AddCustomerAsync(Customer customer);
    public Task<Customer> UpdateCustomerAsync(Customer customer);
    public Task DeleteCustomer(int id);

    public Task<PaginatedList<Customer>> GetCustomersAsync(int pageIndex, int pageSize, string searchTerm = null);
}
