using Castle.Core.Smtp;
using Celestial.ParvazBolourin.Api.Identity.Model;
using Celestial.ParvazBolourin.Api.Identity.Services.JwtToken;
using Celestial.ParvazBolourin.Api.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.ParvazBolourin.Api.Controllers.Account;

public class AccountController : BaseController
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterDto request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = new User
        {
            UserName = request.UserName,
            NationalCode = request.NationalCode,
            PhoneNumber = request.PhoneNumber,
            Name = request.Name,
            LastName = request.LastName,
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors);


        //var confirmationCode = new Random().Next(10000, 99999).ToString();

        //// **ذخیره کد در دیتابیس (SecurityStamp یا یک فیلد اختصاصی)**
        //user.SecurityStamp = confirmationCode;
        //await _userManager.UpdateAsync(user);

        return Ok("User registered.");
    }


    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDto request, [FromServices] JwtTokenService jwtTokenService,CancellationToken cancellationToken)
    {
        var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, request.RememberMe, true);
        if (result.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            var roles = await _userManager.GetRolesAsync(user);
            var token = jwtTokenService.GenerateToken(user,roles, cancellationToken);
            return Ok(new { Token = token });
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