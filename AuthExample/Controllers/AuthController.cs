using AuthExample.Domain.Interfaces;
using AuthExample.Domain.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthExample.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequest request)
    {
        var response = await _authService.Register(request);
        return Created($"/user/{response.Id}",response);
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserRequest request)
    {
        var response = await _authService.Login(request);
        return Ok(response);
    }
}
