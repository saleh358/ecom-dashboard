using System.Threading.Tasks;
using ECom_wep_app.Models;
using ECom_wep_app.Models.Search;
using ECom_wep_app.Models.Utilities;
using ECom_wep_app.Repository.Abstract;
using ECom_wep_app.Repository.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECom_wep_app.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepoitory _customerRepository;

        public CustomerController(ICustomerRepoitory customerRepoitory)
        {
            _customerRepository = customerRepoitory;
        }

        [HttpGet]
        public async Task<List<Customer>> CustomerLockup()
        {
            List<Customer> customers = await _customerRepository.GetAllCustomersAsync();
            return customers;
        }

        public async Task<IActionResult> Details(int id)
        {
            Customer customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View("Details", customer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _customerRepository.AddCustomerAsync(customer);
                return RedirectToAction("List");
            }
            return View("Create", customer);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
                return View("Update", customer);

            var existing = await _customerRepository.GetCustomerByIdAsync(customer.Id);
            if (existing == null)
                return NotFound();

            // map posted fields -> existing entity
            existing.Name = customer.Name;
            existing.Email = customer.Email;
            existing.PhoneNumber = customer.PhoneNumber;
            existing.Address = customer.Address;
            existing.ImageUrl = customer.ImageUrl;

            await _customerRepository.UpdateCustomerAsync(existing);
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _customerRepository.DeleteCustomer(id);
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List(
            CustomerSearchModel search,
            int page = 1,
            int pageSize = 10
        )
        {
            var query = await _customerRepository.CustomerSearchAsync(search);

            var paged = await PaginatedList<Customer>.CreateAsync(
                query.AsNoTracking(),
                page,
                pageSize
            );
            var vm = new CustomerListViewModel { Search = search, Items = paged };
            return View(vm);
        }
    }
}