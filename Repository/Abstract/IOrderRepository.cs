using ECom_wep_app.Models;
using ECom_wep_app.Models.Search;

namespace ECom_wep_app.Repository.Abstract;

public interface IOrderRepository
{
    public Task<List<Order>> GetAllOrdersAsync();
    public Task<Order> GetOrderByIdAsync(int id);
    public Task<Order> AddOrderAsync(Order order);
    public Task<Order> UpdateOrderAsync(Order order);
    public Task DeleteOrder(int id);
    public Task<IQueryable<Order>> OrderSearchAsync(OrderSearchModel model);
}
