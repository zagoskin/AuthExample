using AuthExample.API.Constants;
using AuthExample.Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthExample.API.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme + ", ApiKey")]
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsers();
        return Ok(users);
    }

    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await _userService.GetUserById(id);
        user.Role = null;
        return Ok(user);
    }

    [Authorize(Policy = Identity.RequireAdminRoleName)]
    [HttpGet("{id}/full")]
    public async Task<IActionResult> GetUserByIdAdmin(Guid id)
    {
        var user = await _userService.GetUserById(id);        
        return Ok(user);
    }
}
