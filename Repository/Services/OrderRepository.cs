using ECom_wep_app.Models;
using ECom_wep_app.Models.Search;
using ECom_wep_app.Repository.Abstract;
using Microsoft.EntityFrameworkCore;

namespace ECom_wep_app.Repository.Services;

public class OrderRepository : IOrderRepository
{
    private readonly EComDBContext _context;

    public OrderRepository(EComDBContext context)
    {
        _context = context;
    }

    public async Task<Order> AddOrderAsync(Order order)
    {
        if (order == null)
        {
            throw new ArgumentNullException(nameof(order), "Order cannot be null");
        }

        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
        return order;
    }

    public async Task DeleteOrder(int id)
    {
        var order = await GetOrderByIdAsync(id);
        if (order == null)
        {
            throw new KeyNotFoundException($"Order with ID {id} not found.");
        }

        _context.Orders.Remove(order);
        _context.SaveChanges();
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders.ToListAsync();
    }

    public async Task<Order> GetOrderByIdAsync(int id)
    {
        return _context.Orders.FirstOrDefault(o => o.Id == id);
    }

    public async Task<IQueryable<Order>> OrderSearchAsync(OrderSearchModel model)
    {
        var result = _context.Orders.AsQueryable();

        if (model != null)
        {
            if (model.Id != null)
                result = result.Where(o => o.Id == model.Id);

            if (model.CustomerId != null)
                result = result.Where(o => o.CustomerId == model.CustomerId);

            if (model.ProductId != null)
                result = result.Where(o => o.ProductId == model.ProductId);

            if (model.Quantity != null)
                result = result.Where(o => o.Quantity == model.Quantity);

            if (model.OrderDate != null)
            {
                var date = model.OrderDate.Value.Date;
                result = result.Where(o => o.OrderDate.Date == date);
            }

            result = result.OrderBy(o => o.Id);
        }

        return await Task.FromResult(result);
    }

    public async Task<Order> UpdateOrderAsync(Order order)
    {
        if (order == null)
        {
            throw new ArgumentNullException(nameof(order), "Order cannot be null");
        }

        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
        return order;
    }
}
