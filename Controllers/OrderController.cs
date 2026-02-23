using ECom_wep_app.Models;
using ECom_wep_app.Models.Search;
using ECom_wep_app.Models.Utilities;
using ECom_wep_app.Service.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECom_wep_app.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return View("Details", order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderService.DeleteOrderAsync(id);
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List(
            OrderSearchModel search,
            int page = 1,
            int pageSize = 10
        )
        {
            var query = await _orderService.OrderSearchAsync(search);

            var paged = await PaginatedList<Order>.CreateAsync(
                query.AsNoTracking(),
                page,
                pageSize
            );
            var vm = new OrderListViewModel { Search = search, Orders = paged };
            return View(vm);
        }
    }
}
