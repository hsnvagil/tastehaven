﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taste_Haven_API.Utility;

namespace Taste_Haven_API.Controllers;


[Route("api/AuthTest")]
[ApiController]
public class AuthTestController : ControllerBase
{
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<string>> GetSomething()
    {
        return "You are authenticated";
    }

    [HttpGet("{id:int}")]
    [Authorize(Roles = SD.Role_Admin)]
    public async Task<ActionResult<string>> GetSomething(int someValue)
    {
        return "You are Authorized with Role of Admin";
    }
}
