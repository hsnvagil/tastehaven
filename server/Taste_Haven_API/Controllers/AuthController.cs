#region

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Taste_Haven_API.Models;
using Taste_Haven_API.Models.Dto;
using Taste_Haven_API.Services;
using Taste_Haven_API.Utility;

#endregion

namespace Taste_Haven_API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager,
    IJwtService jwtService)
    : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
    {
        var userFromDb = await userManager.FindByNameAsync(model.UserName);
        if (userFromDb == null || !await userManager.CheckPasswordAsync(userFromDb, model.Password))
            return BadRequest(new { Message = "Username or password is incorrect", IsSuccess = false });

        var roles = await userManager.GetRolesAsync(userFromDb);
        var token = jwtService.GenerateToken(userFromDb, roles);

        return Ok(new { Result = new LoginResponseDto { Email = userFromDb.Email, Token = token }, IsSuccess = true });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto model)
    {
        var userFromDb = await userManager.FindByNameAsync(model.UserName);
        if (userFromDb != null) return BadRequest(new { Message = "Username already exists", IsSuccess = false });

        var newUser = new ApplicationUser
        {
            UserName = model.UserName,
            Email = model.UserName,
            NormalizedEmail = model.UserName.ToUpper(),
            Name = model.Name
        };

        var result = await userManager.CreateAsync(newUser, model.Password);
        if (result.Succeeded)
        {
            if (!await roleManager.RoleExistsAsync(SD.Role_Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(SD.Role_Admin));
                await roleManager.CreateAsync(new IdentityRole(SD.Role_Customer));
            }

            if (model.Role.Equals(SD.Role_Admin, StringComparison.CurrentCultureIgnoreCase))
                await userManager.AddToRoleAsync(newUser, SD.Role_Admin);
            else
                await userManager.AddToRoleAsync(newUser, SD.Role_Customer);

            return Ok(new { Message = "User registered successfully", IsSuccess = true });
        }

        return BadRequest(new { Message = "Error while registering", IsSuccess = false });
    }
}