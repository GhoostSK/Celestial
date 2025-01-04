using Celestial.ParvazBolourin.Api.Identity.Model;
using Celestial.ParvazBolourin.Api.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.ParvazBolourin.Api.Controllers.Account;

public class AccountController : BaseController
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterDto request)
    {
        var user = new User { UserName = request.Email, Email = request.Email, NationalCode = request.NationalCode };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            user.EmailConfirmed = true;
            return Ok();
        }
        return BadRequest(result.Errors);
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDto request)
    {
        var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, request.RememberMe, false);
        if (result.Succeeded)
        {
            return Ok();
        }
        return Unauthorized();
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }

    [HttpGet]
    public IActionResult AccessDenied()
    {
        return Unauthorized("AccessDenied");
    }
}

