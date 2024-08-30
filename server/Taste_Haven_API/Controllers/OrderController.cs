#region

#nullable enable
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taste_Haven_API.Middlewares.Exceptions;
using Taste_Haven_API.Models.Dto;
using Taste_Haven_API.Services;

#endregion

namespace Taste_Haven_API.Controllers;

[Route("api/order")]
[ApiController]
public class OrderController(IOrderService orderService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetOrders(string? userId, string? searchString, string? status, int pageNumber = 1,
        int pageSize = 5)
    {
        try
        {
            var orders = await orderService.GetAllAsync(userId, searchString, status, pageNumber, pageSize);
            return Ok(new { Result = orders, IsSuccess = true });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An unexpected error occurred.", IsSuccess = false });
        }
    }

    [HttpGet("{id:int}", Name = "GetOrder")]
    public async Task<IActionResult> GetOrderById(int id)
    {
        try
        {
            var order = await orderService.GetByIdAsync(id);
            return Ok(new { Result = order, IsSuccess = true });
        }
        catch (InvalidOrderIdException ex)
        {
            return BadRequest(new { ex.Message, IsSuccess = false });
        }
        catch (OrderNotFoundException ex)
        {
            return NotFound(new { ex.Message, IsSuccess = false });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An unexpected error occurred.", IsSuccess = false });
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddOrder([FromBody] OrderHeaderCreateDto orderHeaderDto)
    {
        if (!ModelState.IsValid) return BadRequest(new { Message = "Invalid model data", IsSuccess = false });

        try
        {
            var orderId = await orderService.AddAsync(orderHeaderDto);
            var order = await orderService.GetByIdAsync(orderId);
            return Ok(new { Result = order, IsSuccess = true });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An unexpected error occurred.", IsSuccess = false });
        }
    }

    [Authorize]
    [HttpPut("{id:int}/{status}")]
    public async Task<IActionResult> UpdateOrderStatus(int id, string status)
    {
        try
        {
            await orderService.UpdateAsync(id, status);
            return NoContent();
        }
        catch (OrderNotFoundException ex)
        {
            return NotFound(new { ex.Message, IsSuccess = false });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An unexpected error occurred.", IsSuccess = false });
        }
    }
    
    [Authorize]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        try
        {
            await orderService.DeleteAsync(id);
            return NoContent();
        }
        catch (InvalidOrderIdException ex)
        {
            return BadRequest(new { ex.Message, IsSuccess = false });
        }
        catch (OrderNotFoundException ex)
        {
            return NotFound(new { ex.Message, IsSuccess = false });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An unexpected error occurred.", IsSuccess = false });
        }
    }
}