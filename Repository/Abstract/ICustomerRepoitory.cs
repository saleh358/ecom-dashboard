using ECom_wep_app.Models;

namespace ECom_wep_app.Repository.Abstract;

public interface ICustomerRepoitory
{
    public List<Customer> GetAllCustomers();
    public Customer GetCustomerById(int id);

}
