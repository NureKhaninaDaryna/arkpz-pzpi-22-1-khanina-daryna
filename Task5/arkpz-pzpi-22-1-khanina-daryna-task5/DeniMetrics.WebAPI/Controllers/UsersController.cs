using DeniMetrics.WebAPI.Attributes;
using DineMetrics.BLL.Services.Interfaces;
using DineMetrics.Core.Dto;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace DeniMetrics.WebAPI.Controllers;

public class UsersController : BaseController
{
    private readonly IAuthenticationService _authenticationService;

    public UsersController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register([FromBody] RegisterRequest model)
    {
        return HandleServiceResult(await _authenticationService.Register(model.Email, model.Password));
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthenticateResponseDto>> Login([FromBody] RegisterRequest model)
    {
        return HandleServiceResult(await _authenticationService.Authenticate(model.Email, model.Password));
    }
    
    [HttpPost("change-password")]
    [Authorize]
    public async Task<ActionResult<AuthenticateResponseDto>> Login([FromBody] ChangePasswordRequest model)
    {
        return HandleServiceResult(await _authenticationService.ChangePassword(CurrentUser!.Id ,model.CurrentPassword, model.NewPassword));
    }
}

public class ChangePasswordRequest
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}