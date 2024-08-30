#region

using Microsoft.EntityFrameworkCore;
using Taste_Haven_API.Data;
using Taste_Haven_API.Models;

#endregion

namespace Taste_Haven_API.Repositories;

public interface IPaymentRepository
{
    Task<ShoppingCart> GetShoppingCartByUserIdAsync(string userId);
    Task SaveChangesAsync();
}

public class PaymentRepository(ApplicationDbContext context) : IPaymentRepository
{
    public async Task<ShoppingCart> GetShoppingCartByUserIdAsync(string userId)
    {
        return await context.ShoppingCarts
            .Include(u => u.CartItems)
            .ThenInclude(u => u.MenuItem)
            .FirstOrDefaultAsync(u => u.UserId == userId);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}