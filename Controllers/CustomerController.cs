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

        public IActionResult List()
        {
            List<Customer> customers = _customerRepoitory.GetAllCustomers();
            return View("List", customers);
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

        [HttpPut]
        public IActionResult Update(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerRepoitory.UpdateCustomer(customer);
                return RedirectToAction("List");
            }
            return View("Update", customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _customerRepoitory.DeleteCustomer(id);
            return RedirectToAction("List");
        }
    }
}
