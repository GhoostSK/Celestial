using Celestial.Agency.Api.Areas.Admin.Models.Dto;
using Celestial.Agency.Api.Identity.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Agency.Api.Areas.Admin.Controllers.Account;

public class AccountController : BaseController
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<User> _userManager;

    public AccountController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    [HttpPost]
    public async Task<IActionResult> AddRoleToUser([FromBody] AddRoleToUserDto request)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null) return NotFound($"User with ID {request.UserId} not found.");

        if (!await _roleManager.RoleExistsAsync(request.RoleName)) return BadRequest("Role not found");

        var result = await _userManager.AddToRoleAsync(user, request.RoleName);
        if (result.Succeeded) return Ok($"Role {request.RoleName} added to user {user.UserName}.");

        return BadRequest(result.Errors);
    }

    [HttpPost]
    public async Task<IActionResult> MakeRole([FromBody] MakeRoleDto request)
    {
        var response = await _roleManager.CreateAsync(new IdentityRole(request.RoleName));
        if (response.Succeeded) return Ok();
        return BadRequest(response.Errors);
    }
}