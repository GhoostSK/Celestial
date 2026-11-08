using System.ComponentModel.DataAnnotations;
using Celestial.WebClient.Services.Extensions;

namespace Celestial.WebClient.Services.DTO;

public sealed class AddContactUsReqDto
{
    public string Language { get; set; } = string.Empty;

    [Required(ErrorMessage = "Validation_Required")]
    [StringLength(150, MinimumLength = 3, ErrorMessage = "Validation_Length")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Validation_Required")]
    [EmailAddress(ErrorMessage = "Validation_Email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Validation_Required")]
    [StringLength(2000, MinimumLength = 10, ErrorMessage = "Validation_Length")]
    public string Message { get; set; } = string.Empty;
}

public sealed class AddContactUsResDto : ServiceResContextBase
{
}
