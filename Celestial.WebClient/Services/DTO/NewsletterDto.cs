using System.ComponentModel.DataAnnotations;
using Celestial.WebClient.Services.Extensions;

namespace Celestial.WebClient.Services.DTO;

public sealed class SubscribeNewsletterReqDto
{
    public string Language { get; set; } = string.Empty;

    [Required(ErrorMessage = "Validation_Required")]
    [EmailAddress(ErrorMessage = "Validation_Email")]
    public string Email { get; set; } = string.Empty;
}

public sealed class SubscribeNewsletterResDto : ServiceResContextBase
{
    public long Id { get; set; }
    public bool AlreadySubscribed { get; set; }
}
