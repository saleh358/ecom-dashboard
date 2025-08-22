using ECom_wep_app.Models;
using ECom_wep_app.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;

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
        public List<Customer> CustomerLockup()
        {
            List<Customer> customers = _customerRepoitory.GetAllCustomers();
            return customers;
        }

        public IActionResult Details(int id)
        {
            Customer customer = _customerRepoitory.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View("Details", customer);
        }

        [HttpPost]
        public IActionResult CreateCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerRepoitory.AddCustomer(customer);
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
        public IActionResult UpdateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
                return View("Update", customer);

            var existing = _customerRepoitory.GetCustomerById(customer.Id);
            if (existing == null)
                return NotFound();

            // map posted fields -> existing entity
            existing.Name = customer.Name;
            existing.Email = customer.Email;
            existing.PhoneNumber = customer.PhoneNumber;
            existing.Address = customer.Address;
            existing.ImageUrl = customer.ImageUrl;

            _customerRepoitory.UpdateCustomer(existing);
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var customer = _customerRepoitory.GetCustomerById(id);
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
        public IActionResult List(string searchTerm, int pageIndex = 1, int pageSize = 10)
        {
            var customers = _customerRepoitory.GetCustomers(pageIndex, pageSize, searchTerm);
            return View("List", customers);
        }

    }
}
