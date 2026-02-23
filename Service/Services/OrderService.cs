using ECom_wep_app.Models;
using ECom_wep_app.Models.Search;
using ECom_wep_app.Repository.Abstract;
using ECom_wep_app.Service.Abstract;

namespace ECom_wep_app.Service.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public Task<List<Order>> GetAllOrdersAsync()
    {
        return _orderRepository.GetAllOrdersAsync();
    }

    public Task<Order> GetOrderByIdAsync(int id)
    {
        return _orderRepository.GetOrderByIdAsync(id);
    }

    public Task<Order> AddOrderAsync(Order order)
    {
        return _orderRepository.AddOrderAsync(order);
    }

    public Task<Order> UpdateOrderAsync(Order order)
    {
        return _orderRepository.UpdateOrderAsync(order);
    }

    public Task DeleteOrderAsync(int id)
    {
        return _orderRepository.DeleteOrder(id);
    }

    public Task<IQueryable<Order>> OrderSearchAsync(OrderSearchModel model)
    {
        return _orderRepository.OrderSearchAsync(model);
    }
}
