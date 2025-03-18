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

        var existingUser = await _userManager.FindByNameAsync(request.UserName);
        if (existingUser != null)
        {
            return BadRequest("User already exists.");
        }

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

        return Ok("User registered successfully. Please verify your phone number.");
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDto request, [FromServices] JwtTokenService jwtTokenService, CancellationToken cancellationToken)
    {
        var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, request.RememberMe, true);

        if (result.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user!.PhoneNumberConfirmed == false)
            {
                return StatusCode(403, "Your phone number is not confirmed. Please verify it first.");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var token = jwtTokenService.GenerateToken(user, roles, cancellationToken);
            return Ok(new { Token = token });
        }
        return Unauthorized("Username or password not valid");
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
    public async Task<IActionResult> SendConfirmationCode([FromBody] PhoneNumberDto request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user == null)
            return NotFound("User not found");

        if (user.PhoneNumberConfirmed)
            return BadRequest("Your phone number is already confirmed. No need to verify again.");

        // محدودیت زمانی برای جلوگیری از سوءاستفاده
        if (user.ConfirmationCodeRequestedAt.HasValue &&
            (DateTime.UtcNow - user.ConfirmationCodeRequestedAt.Value).TotalMinutes < 2)
        {
            return BadRequest("Please wait before requesting a new confirmation code.");
        }

        // تولید کد ۵ رقمی غیر از 00000
        string confirmationCode;
        do
        {
            confirmationCode = new Random().Next(10000, 99999).ToString();
        } while (confirmationCode == "00000");

        user.PhoneNumberConfirmationCode = confirmationCode;
        user.ConfirmationCodeRequestedAt = DateTime.UtcNow;

        await _userManager.UpdateAsync(user);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("--------------------------Code--------------------------");
        Console.WriteLine(confirmationCode);
        Console.WriteLine("--------------------------Code--------------------------");
        Console.ForegroundColor = ConsoleColor.White;
        return Ok("Confirmation code sent.");
    }



    [HttpPost]
    public async Task<IActionResult> ConfirmPhoneNumber([FromBody] ConfirmPhoneDto request)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        if (user == null)
            return NotFound("User not found");

        if (user.PhoneNumberConfirmed)
            return BadRequest("Your phone number is already confirmed.");

        if (user.LockedUntil.HasValue && user.LockedUntil > DateTime.UtcNow)
        {
            return BadRequest($"Too many failed attempts. Try again after {(user.LockedUntil.Value - DateTime.UtcNow).Minutes} minutes.");
        }

        if (user.PhoneNumberConfirmationCode == null || user.PhoneNumberConfirmationCode != request.Code)
        {
            user.FailedConfirmationAttempts++;

            if (user.FailedConfirmationAttempts >= 3)
            {
                user.LockedUntil = DateTime.UtcNow.AddMinutes(5);
                user.FailedConfirmationAttempts = 0;
            }

            await _userManager.UpdateAsync(user);
            return BadRequest("Invalid confirmation code.");
        }

        // تأیید شماره تلفن
        user.PhoneNumberConfirmed = true;
        user.PhoneNumberConfirmationCode = null;
        user.FailedConfirmationAttempts = 0;

        await _userManager.UpdateAsync(user);
        return Ok("Phone number confirmed successfully.");
    }

}