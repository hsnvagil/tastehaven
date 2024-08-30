#region

using AutoMapper;
using Taste_Haven_API.Models;
using Taste_Haven_API.Models.Dto;
using Taste_Haven_API.Repositories;

#endregion

namespace Taste_Haven_API.Services;

public interface IShoppingCartService
{
    Task<ShoppingCartDto> GetShoppingCartAsync(string userId);
    Task<ShoppingCartDto> AddOrUpdateItemInCartAsync(string userId, int menuItemId, int updateQuantityBy);
}

public class ShoppingCartService(IShoppingCartRepository shoppingCartRepository, IMapper mapper)
    : IShoppingCartService
{
    public async Task<ShoppingCartDto> GetShoppingCartAsync(string userId)
    {
        var shoppingCart = await shoppingCartRepository.GetShoppingCartByUserIdAsync(userId);
        if (shoppingCart is { CartItems.Count: > 0 })
            shoppingCart.CartTotal = shoppingCart.CartItems.Sum(u => u.Quantity * u.MenuItem.Price);
        return mapper.Map<ShoppingCartDto>(shoppingCart);
    }

    public async Task<ShoppingCartDto> AddOrUpdateItemInCartAsync(string userId, int menuItemId, int updateQuantityBy)
    {
        var shoppingCart = await shoppingCartRepository.GetShoppingCartByUserIdAsync(userId);

        if (shoppingCart == null && updateQuantityBy > 0)
        {
            shoppingCart = new ShoppingCart { UserId = userId };
            await shoppingCartRepository.AddShoppingCartAsync(shoppingCart);
            await shoppingCartRepository.SaveChangesAsync();

            var newCartItem = new CartItem
            {
                MenuItemId = menuItemId,
                Quantity = updateQuantityBy,
                ShoppingCartId = shoppingCart.Id,
                MenuItem = null
            };
            await shoppingCartRepository.AddCartItemAsync(newCartItem);
        }
        else if (shoppingCart != null)
        {
            var cartItem = await shoppingCartRepository.GetCartItemAsync(shoppingCart.Id, menuItemId);
            if (cartItem == null)
            {
                var newCartItem = new CartItem
                {
                    MenuItemId = menuItemId,
                    Quantity = updateQuantityBy,
                    ShoppingCartId = shoppingCart.Id,
                    MenuItem = null
                };
                await shoppingCartRepository.AddCartItemAsync(newCartItem);
            }
            else
            {
                var newQuantity = cartItem.Quantity + updateQuantityBy;
                if (updateQuantityBy == 0 || newQuantity <= 0)
                {
                    await shoppingCartRepository.RemoveCartItemAsync(cartItem);
                    if (shoppingCart.CartItems.Count == 1)
                        await shoppingCartRepository.RemoveShoppingCartAsync(shoppingCart);
                }
                else
                {
                    cartItem.Quantity = newQuantity;
                }
            }
        }

        await shoppingCartRepository.SaveChangesAsync();
        return mapper.Map<ShoppingCartDto>(shoppingCart);
    }
}