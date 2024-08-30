#region

using Microsoft.EntityFrameworkCore;
using Taste_Haven_API.Data;
using Taste_Haven_API.Models;

#endregion

namespace Taste_Haven_API.Repositories;

public interface IShoppingCartRepository
{
    Task<ShoppingCart> GetShoppingCartByUserIdAsync(string userId);
    Task<CartItem> GetCartItemAsync(int shoppingCartId, int menuItemId);
    Task AddShoppingCartAsync(ShoppingCart shoppingCart);
    Task AddCartItemAsync(CartItem cartItem);
    Task RemoveCartItemAsync(CartItem cartItem);
    Task RemoveShoppingCartAsync(ShoppingCart shoppingCart);
    Task SaveChangesAsync();
}

public class ShoppingCartRepository(ApplicationDbContext context) : IShoppingCartRepository
{
    public async Task<ShoppingCart> GetShoppingCartByUserIdAsync(string userId)
    {
        return await context.ShoppingCarts
            .Include(u => u.CartItems)
            .ThenInclude(u => u.MenuItem)
            .FirstOrDefaultAsync(u => u.UserId == userId);
    }

    public async Task<CartItem> GetCartItemAsync(int shoppingCartId, int menuItemId)
    {
        return await context.CartItems.FirstOrDefaultAsync(ci =>
            ci.ShoppingCartId == shoppingCartId && ci.MenuItemId == menuItemId);
    }

    public async Task AddShoppingCartAsync(ShoppingCart shoppingCart)
    {
        await context.ShoppingCarts.AddAsync(shoppingCart);
    }

    public async Task AddCartItemAsync(CartItem cartItem)
    {
        await context.CartItems.AddAsync(cartItem);
    }

    public Task RemoveCartItemAsync(CartItem cartItem)
    {
        context.CartItems.Remove(cartItem);
        return Task.CompletedTask;
    }

    public Task RemoveShoppingCartAsync(ShoppingCart shoppingCart)
    {
        context.ShoppingCarts.Remove(shoppingCart);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}