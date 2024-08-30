#region

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taste_Haven_API.Services;

#endregion

namespace Taste_Haven_API.Controllers
{
    [Route("api/shopping-cart")]
    [ApiController]
    public class ShoppingCartController(IShoppingCartService shoppingCartService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetShoppingCart(string userId)
        {
            try
            {
                var shoppingCart = await shoppingCartService.GetShoppingCartAsync(userId);
                return Ok(new {Result = shoppingCart});
            }
            catch (Exception ex)
            {
                return BadRequest(new {ErrorMessages = new List<string> {ex.Message}, IsSuccess = false});
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> AddOrUpdateItemInCart(string userId, int menuItemId, int updateQuantityBy)
        {
            try
            {
                var response = await shoppingCartService.AddOrUpdateItemInCartAsync(userId, menuItemId, updateQuantityBy);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new {ErrorMessages = new List<string> {ex.Message}, IsSuccess = false});
            }
        }
    }
}
