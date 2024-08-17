using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Taste_Haven_API.Data;
using Taste_Haven_API.Models;

namespace Taste_Haven_API.Controllers;

[Route("api/ShoppingCart")]
[ApiController]
public class ShoppingCartController : ControllerBase
{
	protected ApiResponse _response;
	private readonly ApplicationDbContext _db;
	public ShoppingCartController(ApplicationDbContext db)
	{
		_response = new();
		_db = db;
	}


	[HttpPost]
	public async Task<ActionResult<ApiResponse>> AddOrUpdateItemInCart(string userId, int menuItemId, int updateQuantityBy)
	{
		ShoppingCart shoppingCart = _db.ShoppingCarts
			.Include(u => u.CartItems)
			.FirstOrDefault(u => u.UserId == userId);
		MenuItem menuItem = _db.MenuItems.FirstOrDefault(u => u.Id == menuItemId);
		
		if(menuItem == null)
		{
			_response.StatusCode = HttpStatusCode.BadRequest;
			_response.IsSuccess = false;
			return BadRequest(_response);
		}

		if(shoppingCart == null && updateQuantityBy > 0)
		{
			ShoppingCart newCart = new() { UserId = userId };
			_db.ShoppingCarts.Add(newCart);
			_db.SaveChanges();

			CartItem newCartItem = new()
			{
				MenuItemId = menuItemId,
				Quantity = updateQuantityBy,
				ShoppingCartId = newCart.Id,
				MenuItem = null
			};
			_db.CartItems.Add(newCartItem);
			_db.SaveChanges();
		}
		else
		{
			CartItem cartItemInCart = shoppingCart.CartItems.FirstOrDefault(u => u.MenuItemId == menuItemId);
			if(cartItemInCart == null)
			{
				CartItem newCartItem = new()
				{
					MenuItemId = menuItemId,
					Quantity = updateQuantityBy,
					ShoppingCartId = shoppingCart.Id,
					MenuItem = null
				};
				_db.CartItems.Add(newCartItem);
				_db.SaveChanges();
			}
			else
			{
				int newQuantity = cartItemInCart.Quantity + updateQuantityBy;
				if(updateQuantityBy == 0 || newQuantity <= 0)
				{
					_db.CartItems.Remove(cartItemInCart);
					if(shoppingCart.CartItems.Count() == 1)
					{
						_db.ShoppingCarts.Remove(shoppingCart);
					}
					_db.SaveChanges();
				}
				else
				{
					cartItemInCart.Quantity = newQuantity;
					_db.SaveChanges();
				}
			}
		}
		return _response;
	}
}
