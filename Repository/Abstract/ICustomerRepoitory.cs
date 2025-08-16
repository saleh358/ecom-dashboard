using ECom_wep_app.Models;

namespace ECom_wep_app.Repository.Abstract;

public interface ICustomerRepoitory
{
    public List<Customer> GetAllCustomers();
    public Customer GetCustomerById(int id);
    public Customer AddCustomer(Customer customer);
    public Customer UpdateCustomer(Customer customer);
    public void DeleteCustomer(int id);

    public PaginatedList<Customer> GetCustomers(int pageIndex, int pageSize, string searchTerm = null);
}
