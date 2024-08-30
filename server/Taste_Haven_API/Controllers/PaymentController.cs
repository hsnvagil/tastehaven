#region

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taste_Haven_API.Services;

#endregion

namespace Taste_Haven_API.Controllers;

[Route("api/payment")]
[ApiController]
public class PaymentController(IPaymentService paymentService) : ControllerBase
{
    [Authorize]
    [HttpPost]
    public async Task<ActionResult> MakePayment(string userId)
    {
        try
        {
            var shoppingCart = await paymentService.ProcessPayment(userId);
            return Ok(new { Result = shoppingCart, IsSuccess = true });
        }
        catch (Exception ex)
        {
            return BadRequest(new { ex.Message, IsSuccess = false });
        }
    }
}