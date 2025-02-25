using Castle.Core.Smtp;
using Celestial.ParvazBolourin.Api.Identity.Model;
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

    //[HttpPost("register")]
    //public async Task<IActionResult> Register([FromBody] RegisterDto request)
    //{
    //    if (!ModelState.IsValid)
    //        return BadRequest(ModelState);

    //    var user = new User
    //    {
    //        UserName = request.Email,
    //        Email = request.Email,
    //        NationalCode = request.NationalCode
    //    };

    //    var result = await _userManager.CreateAsync(user, request.Password);
    //    if (!result.Succeeded)
    //        return BadRequest(result.Errors);

    //    // **ایجاد کد تأیید عددی ۵ رقمی**
    //    var confirmationCode = new Random().Next(10000, 99999).ToString();

    //    // **ذخیره کد در دیتابیس (SecurityStamp یا یک فیلد اختصاصی)**
    //    user.SecurityStamp = confirmationCode;
    //    await _userManager.UpdateAsync(user);

    //    // **ارسال ایمیل**
    //    await _emailSender.SendEmailAsync(user.Email, "Email Verification Code",
    //        $"Your verification code is: <strong>{confirmationCode}</strong>");

    //    return Ok("User registered. Please check your email for the verification code.");
    //}

    //[HttpPost("confirm-email")]
    //public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailDto model)
    //{
    //    var user = await _userManager.FindByIdAsync(model.UserId);
    //    if (user == null)
    //        return NotFound("User not found.");

    //    if (user.SecurityStamp != model.Code)
    //        return BadRequest("Invalid verification code.");

    //    // **تأیید ایمیل**
    //    user.EmailConfirmed = true;
    //    user.SecurityStamp = null; // حذف کد تأیید پس از استفاده
    //    await _userManager.UpdateAsync(user);

    //    return Ok("Email confirmed successfully.");
    //}

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDto request)
    {
        var result =
            await _signInManager.PasswordSignInAsync(request.Email, request.Password, request.RememberMe, false);
        if (result.Succeeded) return Ok();
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