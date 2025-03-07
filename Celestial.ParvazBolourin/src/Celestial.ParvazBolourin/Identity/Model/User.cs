using Microsoft.AspNetCore.Identity;

namespace Celestial.ParvazBolourin.Api.Identity.Model;

public class User : IdentityUser
{
    public string NationalCode { get; set; } = string.Empty;
    public string Name { get; set; }
    public string LastName { get; set; }
}