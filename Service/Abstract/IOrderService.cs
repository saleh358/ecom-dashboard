using ECom_wep_app.Models;
using ECom_wep_app.Models.Search;

namespace ECom_wep_app.Service.Abstract;

public interface IOrderService
{
    Task<List<Order>> GetAllOrdersAsync();
    Task<Order> GetOrderByIdAsync(int id);
    Task<Order> AddOrderAsync(Order order);
    Task<Order> UpdateOrderAsync(Order order);
    Task DeleteOrderAsync(int id);
    Task<IQueryable<Order>> OrderSearchAsync(OrderSearchModel model);
}
