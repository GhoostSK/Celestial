using System.ComponentModel.DataAnnotations;
using Celestial.WebClient.Services.Extensions;

namespace Celestial.WebClient.Services.DTO;

// ---------------------- Requests ----------------------

public sealed class LoginReqDto
{
    public string Language { get; set; } = string.Empty;

    [Required(ErrorMessage = "Validation_Required")]
    [RegularExpression(@"^[a-zA-Z0-9._]+$", ErrorMessage = "Validation_UserName")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Validation_Required")]
    public string Password { get; set; } = string.Empty;

    public bool RememberMe { get; set; }
}

public sealed class RegisterReqDto
{
    public string Language { get; set; } = string.Empty;

    [Required(ErrorMessage = "Validation_Required")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Validation_Length")]
    [RegularExpression(@"^[a-zA-Z0-9._]+$", ErrorMessage = "Validation_UserName")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Validation_Required")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "Validation_Password")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Validation_Required")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Validation_NationalCode")]
    public string NationalCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Validation_Required")]
    [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Validation_Phone")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Validation_Required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Validation_Length")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Validation_Required")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Validation_Length")]
    public string LastName { get; set; } = string.Empty;
}

public sealed class SendConfirmationCodeReqDto
{
    public string Language { get; set; } = string.Empty;

    [Required(ErrorMessage = "Validation_Required")]
    public string UserName { get; set; } = string.Empty;
}

public sealed class ConfirmPhoneReqDto
{
    public string Language { get; set; } = string.Empty;

    [Required(ErrorMessage = "Validation_Required")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Validation_Required")]
    [RegularExpression(@"^\d{5}$", ErrorMessage = "Validation_ConfirmationCode")]
    public string Code { get; set; } = string.Empty;
}

// ---------------------- Responses ----------------------

public sealed class LoginResDto : ServiceResContextBase
{
    public string Token { get; set; } = string.Empty;
}

/// <summary>
///     پاسخ اندپوینت‌هایی که فقط یک پیام متنی برمی‌گردانند (ثبت‌نام، ارسال و تأیید کد).
/// </summary>
public sealed class ApiMessageResDto : ServiceResContextBase
{
    public string Message { get; set; } = string.Empty;
}
