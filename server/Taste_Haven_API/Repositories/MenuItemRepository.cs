#region

using Microsoft.EntityFrameworkCore;
using Taste_Haven_API.Data;
using Taste_Haven_API.Models;

#endregion

namespace Taste_Haven_API.Repositories;

public interface IMenuItemRepository
{
    Task<MenuItem> GetByIdAsync(int id);
    Task<int> AddAsync(MenuItem menuItem);
    Task UpdateAsync(MenuItem menuItem);
    Task<IEnumerable<MenuItem>> GetAllAsync();
    Task DeleteAsync(MenuItem menuItem);
}

public class MenuItemRepository(ApplicationDbContext context) : IMenuItemRepository
{
    public async Task<MenuItem> GetByIdAsync(int id)
    {
        return await context.MenuItems.FindAsync(id);
    }

    public async Task<int> AddAsync(MenuItem menuItem)
    {
        context.MenuItems.Add(menuItem);
        await context.SaveChangesAsync();
        return menuItem.Id;
    }

    public async Task UpdateAsync(MenuItem menuItem)
    {
        context.MenuItems.Update(menuItem);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<MenuItem>> GetAllAsync()
    {
        return await context.MenuItems.ToListAsync();
    }

    public async Task DeleteAsync(MenuItem entity)
    {
        context.MenuItems.Remove(entity);
        await context.SaveChangesAsync();
    }
}