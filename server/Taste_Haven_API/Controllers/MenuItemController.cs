#region

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taste_Haven_API.Middlewares.Exceptions;
using Taste_Haven_API.Models.Dto;
using Taste_Haven_API.Services;

#endregion

namespace Taste_Haven_API.Controllers;

[Route("api/menu-item")]
[ApiController]
public class MenuItemController(IMenuItemService menuItemService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetMenuItems()
    {
        try
        {
            var menuItems = await menuItemService.GetAllAsync();
            return Ok(new { Result = menuItems, IsSuccess = true });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An unexpected error occurred.", IsSuccess = false });
        }
    }

    [HttpGet("{id:int}", Name = "GetMenuItem")]
    public async Task<IActionResult> GetMenuItemById(int id)
    {
        try
        {
            var menuItem = await menuItemService.GetByIdAsync(id);
            return Ok(new { Result = menuItem, IsSuccess = true });
        }
        catch (InvalidMenuItemIdException ex)
        {
            return BadRequest(new { ex.Message, IsSuccess = false });
        }
        catch (MenuItemNotFoundException ex)
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
    public async Task<ActionResult> AddMenuItem([FromForm] MenuItemCreateDto menuItemCreateDto)
    {
        if (!ModelState.IsValid) return BadRequest(new { Message = "Model is not valid", IsSuccess = false });

        try
        {
            var menuItemId = await menuItemService.AddAsync(menuItemCreateDto);
            return CreatedAtRoute("GetMenuItem", new { id = menuItemId },
                new { Result = menuItemId, IsSuccess = true });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { ex.Message, IsSuccess = false });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ex.Message, IsSuccess = false });
        }
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateMenuItem(int id, [FromForm] MenuItemUpdateDto menuItemUpdateDto)
    {
        if (!ModelState.IsValid) return BadRequest(new { Message = "Invalid model data", IsSuccess = false });

        try
        {
            await menuItemService.UpdateAsync(id, menuItemUpdateDto);
            return NoContent();
        }
        catch (MenuItemNotFoundException ex)
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
    public async Task<ActionResult> DeleteMenuItem(int id)
    {
        try
        {
            await menuItemService.DeleteAsync(id);
            return NoContent();
        }
        catch (InvalidMenuItemIdException ex)
        {
            return BadRequest(new { ex.Message, IsSuccess = false });
        }
        catch (MenuItemNotFoundException ex)
        {
            return NotFound(new { ex.Message, IsSuccess = false });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An unexpected error occurred.", IsSuccess = false });
        }
    }
}