using ECom_wep_app.Models;
using ECom_wep_app.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECom_wep_app.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepoitory _customerRepoitory;

        public CustomerController(ICustomerRepoitory customerRepoitory)
        {
            _customerRepoitory = customerRepoitory;
        }
        [HttpGet]
        public async Task<List<Customer>> CustomerLockup()
        {
            List<Customer> customers = await _customerRepoitory.GetAllCustomersAsync();
            return customers;
        }

        public async Task<IActionResult> Details(int id)
        {
            Customer customer = await _customerRepoitory.GetCustomerByIdAsync(id);
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
               await _customerRepoitory.AddCustomerAsync(customer);
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

            var existing = await _customerRepoitory.GetCustomerByIdAsync(customer.Id);
            if (existing == null)
                return NotFound();

            // map posted fields -> existing entity
            existing.Name = customer.Name;
            existing.Email = customer.Email;
            existing.PhoneNumber = customer.PhoneNumber;
            existing.Address = customer.Address;
            existing.ImageUrl = customer.ImageUrl;

            await _customerRepoitory.UpdateCustomerAsync(existing);
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var customer =await _customerRepoitory.GetCustomerByIdAsync(id);
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _customerRepoitory.DeleteCustomer(id);
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List(string searchTerm, int pageIndex = 1, int pageSize = 10)
        {
            var customers =await _customerRepoitory.GetCustomersAsync(pageIndex, pageSize, searchTerm);
            return View("List", customers);
        }

    }
}
