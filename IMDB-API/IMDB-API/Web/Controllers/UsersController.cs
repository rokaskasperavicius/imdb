using System.ComponentModel.DataAnnotations;
using IMDB_API.Application.DTOs;
using IMDB_API.Application.Services;
using IMDB_API.Web.Common;
using Microsoft.AspNetCore.Mvc;

namespace IMDB_API.Web.Controllers;

public class RegisterRequest
{
    [Required] public string Name { get; set; }

    [Required] [EmailAddress] public string Email { get; set; }

    [Required] public string Password { get; set; }
}

public class LoginRequest
{
    [Required] [EmailAddress] public string Email { get; set; }

    [Required] public string Password { get; set; }
}

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    // POST: api/users/register
    [HttpPost("register")]
    public ActionResult<UserDto> RegisterUser(RegisterRequest user)
    {
        try
        {
            var registeredUser = _usersService.RegisterUser(
                user.Name,
                user.Email,
                user.Password);

            return Created($"/users/{registeredUser.Id}", registeredUser);
        }
        catch (Exception ex)
        {
            return Conflict(new ErrorMessage
            {
                Message = ex.Message
            });
        }
    }

    // POST: api/users/login
    [HttpPost("login")]
    public ActionResult<UserWithTokenDto> LoginUser(LoginRequest user)
    {
        try
        {
            var authUser = _usersService.LoginUser(
                user.Email,
                user.Password);

            return Ok(authUser);
        }
        catch (Exception ex)
        {
            return BadRequest(new ErrorMessage
            {
                Message = ex.Message
            });
        }
    }
}