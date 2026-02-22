using ECom_wep_app.Models;
using ECom_wep_app.Models.Search;
using ECom_wep_app.Models.Utilities;
using ECom_wep_app.Repository.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECom_wep_app.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return View("Details", order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            if (ModelState.IsValid)
            {
                await _orderRepository.AddOrderAsync(order);
                return RedirectToAction("List");
            }

            return View("Create", order);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateOrder(Order order)
        {
            if (!ModelState.IsValid)
                return View("Update", order);

            var existing = await _orderRepository.GetOrderByIdAsync(order.Id);
            if (existing == null)
                return NotFound();

            existing.CustomerId = order.CustomerId;
            existing.ProductId = order.ProductId;
            existing.Quantity = order.Quantity;
            existing.OrderDate = order.OrderDate;

            await _orderRepository.UpdateOrderAsync(existing);
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _orderRepository.DeleteOrder(id);
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List(OrderSearchModel search, int page = 1, int pageSize = 10)
        {
            var query = await _orderRepository.OrderSearchAsync(search);

            var paged = await PaginatedList<Order>.CreateAsync(query.AsNoTracking(), page, pageSize);
            var vm = new OrderListViewModel { Search = search, Orders = paged };
            return View(vm);
        }
    }
}
