using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Celestial.Agency.Api.Models.Dto;

public sealed class SubscribeNewsletterReqDto
{
    [FromRoute] public string Language { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "The email address is not valid.")]
    public string Email { get; set; } = string.Empty;
}

public sealed class UnsubscribeNewsletterReqDto
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "The email address is not valid.")]
    public string Email { get; set; } = string.Empty;
}

public sealed class GetNewsletterSubscriptionListReqDto
{
    public bool OnlyActive { get; set; } = true;
    public int PageSize { get; set; } = 20;
    public int PageNumber { get; set; } = 1;
}
