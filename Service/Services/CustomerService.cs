using ECom_wep_app.Models;
using ECom_wep_app.Models.Search;
using ECom_wep_app.Repository.Abstract;
using ECom_wep_app.Service.Abstract;

namespace ECom_wep_app.Service.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepoitory _customerRepository;

    public CustomerService(ICustomerRepoitory customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public Task<List<Customer>> GetAllCustomersAsync()
    {
        return _customerRepository.GetAllCustomersAsync();
    }

    public Task<Customer> GetCustomerByIdAsync(int id)
    {
        return _customerRepository.GetCustomerByIdAsync(id);
    }

    public Task<Customer> AddCustomerAsync(Customer customer)
    {
        return _customerRepository.AddCustomerAsync(customer);
    }

    public Task<Customer> UpdateCustomerAsync(Customer customer)
    {
        return _customerRepository.UpdateCustomerAsync(customer);
    }

    public Task DeleteCustomerAsync(int id)
    {
        return _customerRepository.DeleteCustomer(id);
    }

    public Task<IQueryable<Customer>> CustomerSearchAsync(CustomerSearchModel model)
    {
        return _customerRepository.CustomerSearchAsync(model);
    }
}
