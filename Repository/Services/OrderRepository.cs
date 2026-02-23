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
        await ValidateOrderForCreateOrUpdateAsync(order);

        var normalizedOrder = new Order
        {
            CustomerId = order.CustomerId,
            OrderDate = order.OrderDate == default ? DateTime.UtcNow : order.OrderDate,
            OrderItems = order.OrderItems
                .Select(i => new OrderItem { ProductId = i.ProductId, Quantity = i.Quantity })
                .ToList(),
        };

        await _context.Orders.AddAsync(normalizedOrder);
        await _context.SaveChangesAsync();
        return normalizedOrder;
    }

    public async Task DeleteOrder(int id)
    {
        var order = await GetOrderByIdAsync(id);
        if (order == null)
        {
            throw new KeyNotFoundException($"Order with ID {id} not found.");
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders.Include(o => o.OrderItems).ToListAsync();
    }

    public async Task<Order> GetOrderByIdAsync(int id)
    {
        return await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<IQueryable<Order>> OrderSearchAsync(OrderSearchModel model)
    {
        var result = _context.Orders.Include(o => o.OrderItems).AsQueryable();

        if (model != null)
        {
            if (model.Id != null)
                result = result.Where(o => o.Id == model.Id);

            if (model.CustomerId != null)
                result = result.Where(o => o.CustomerId == model.CustomerId);

            if (model.OrderDate != null)
            {
                var date = model.OrderDate.Value.Date;
                result = result.Where(o => o.OrderDate.Date == date);
            }

            result = result.OrderBy(o => o.Id);
        }
        else
        {
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

        await ValidateOrderForCreateOrUpdateAsync(order);

        var existingOrder = await _context
            .Orders.Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == order.Id);

        if (existingOrder == null)
        {
            throw new KeyNotFoundException($"Order with ID {order.Id} not found.");
        }

        existingOrder.CustomerId = order.CustomerId;
        existingOrder.OrderDate = order.OrderDate == default ? existingOrder.OrderDate : order.OrderDate;

        _context.OrderItems.RemoveRange(existingOrder.OrderItems);
        existingOrder.OrderItems = order.OrderItems
            .Select(i => new OrderItem { ProductId = i.ProductId, Quantity = i.Quantity })
            .ToList();

        await _context.SaveChangesAsync();
        return existingOrder;
    }

    private async Task ValidateOrderForCreateOrUpdateAsync(Order order)
    {
        if (order == null)
        {
            throw new ArgumentNullException(nameof(order), "Order cannot be null");
        }

        if (order.CustomerId <= 0)
        {
            throw new ArgumentException("Order must have a valid CustomerId.", nameof(order));
        }

        var customerExists = await _context.Customers.AnyAsync(c => c.Id == order.CustomerId);
        if (!customerExists)
        {
            throw new ArgumentException(
                $"Customer with ID {order.CustomerId} does not exist.",
                nameof(order)
            );
        }

        if (order.OrderItems == null || order.OrderItems.Count == 0)
        {
            throw new ArgumentException("Order must contain at least one OrderItem.", nameof(order));
        }

        if (order.OrderItems.Any(i => i.ProductId <= 0 || i.Quantity <= 0))
        {
            throw new ArgumentException(
                "Each OrderItem must include a valid ProductId and Quantity greater than zero.",
                nameof(order)
            );
        }

        var distinctProductIds = order.OrderItems.Select(i => i.ProductId).Distinct().ToList();
        var existingProductsCount = await _context.Products.CountAsync(p =>
            distinctProductIds.Contains(p.Id)
        );

        if (existingProductsCount != distinctProductIds.Count)
        {
            throw new ArgumentException("Order contains one or more invalid ProductId values.", nameof(order));
        }
    }
}
