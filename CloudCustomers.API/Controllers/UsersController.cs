using CloudCustomers.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudCustomers.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    // private readonly ILogger<UsersController> _logger;
    private readonly IUsersService _userService;

    public UsersController(IUsersService userService)
    {
        // _logger = logger;
        _userService = userService;
    }

    [HttpGet(Name = "GetUsers")]
    public async Task<IActionResult> Get()
    {
        var users = await _userService.GetAllUsers();
        if (!users.Any()) return NotFound();
        return Ok(users);
    }
}
