#region

using Microsoft.EntityFrameworkCore;
using Taste_Haven_API.Data;
using Taste_Haven_API.Models;

#endregion

namespace Taste_Haven_API.Repositories;

public interface IOrderRepository
{
    Task<OrderHeader> GetByIdAsync(int id);
    Task<int> AddAsync(OrderHeader orderHeader);
    Task UpdateAsync(OrderHeader orderHeader);
    Task<IEnumerable<OrderHeader>> GetAllAsync();
    Task DeleteAsync(OrderHeader orderHeader);
}

public class OrderRepository(ApplicationDbContext context) : IOrderRepository
{
    public async Task<OrderHeader> GetByIdAsync(int id)
    {
        return await context.OrderHeaders.Include(o => o.OrderDetails)
            .ThenInclude(od => od.MenuItem)
            .FirstOrDefaultAsync(o => o.OrderHeaderId == id);
    }

    public async Task<int> AddAsync(OrderHeader orderHeader)
    {
        await context.OrderHeaders.AddAsync(orderHeader);
        foreach (var details in orderHeader.OrderDetails)
        {
            await context.OrderDetails.AddAsync(details);
        }
        await context.SaveChangesAsync();
        return orderHeader.OrderHeaderId;
    }

    public async Task UpdateAsync(OrderHeader orderHeader)
    {
        context.OrderHeaders.Update(orderHeader);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<OrderHeader>> GetAllAsync()
    {
        return await context.OrderHeaders
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.MenuItem)
            .ToListAsync();
    }

    public async Task DeleteAsync(OrderHeader orderHeader)
    {
        context.OrderHeaders.Remove(orderHeader);
        await context.SaveChangesAsync();
    }
}