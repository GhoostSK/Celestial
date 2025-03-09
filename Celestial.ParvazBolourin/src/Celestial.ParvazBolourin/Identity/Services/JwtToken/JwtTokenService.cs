using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Celestial.ParvazBolourin.Api.Identity.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Celestial.ParvazBolourin.Api.Identity.Services.JwtToken;

public class JwtTokenService
{
    private readonly IConfiguration _configuration;


    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> GenerateToken(User user, IList<string> roles, CancellationToken cancellationToken)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName!)
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // استفاده از Task.Run برای اجرای async و پشتیبانی از CancellationToken
        var token = await Task.Run(() =>
        {
            cancellationToken.ThrowIfCancellationRequested();

            var jwtToken = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }, cancellationToken);

        return token;
    }

}
