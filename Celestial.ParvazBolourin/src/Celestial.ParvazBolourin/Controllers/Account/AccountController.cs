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

        //// **ارسال ایمیل**
        //await _emailSender.SendEmailAsync(user.Email, "Email Verification Code",
        //    $"Your verification code is: <strong>{confirmationCode}</strong>");

        return Ok("User registered. Please check your email for the verification code.");
    }


    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDto request, [FromServices] JwtTokenService jwtTokenService)
    {
        var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, request.RememberMe, true);
        if (result.Succeeded)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            var token = jwtTokenService.GenerateToken(user);
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

    [HttpPost]
    public IActionResult SendEmail([FromServices] IEmailSender emailSender)
    {
        emailSender.Send("sadrakhoshnavaz709@gmail.com", "khoshnavazsadra@gmail.com", "Test Email", "Hello from API");
        return Ok("Email sent.");
    }
}