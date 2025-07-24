using System.ComponentModel.DataAnnotations;

namespace Celestial.Agency.Api.Models.Dto;

public class RegisterDto
{
    [Required(ErrorMessage = "Username is required.")]
    [StringLength(30, ErrorMessage = "Username must be between {2} and {1} characters.", MinimumLength = 3)]
    [RegularExpression(@"^[a-zA-Z0-9._]+$",
        ErrorMessage = "Username can only contain letters, numbers, dots, and underscores.")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, ErrorMessage = "Password must be at least {2} characters long.", MinimumLength = 8)]
    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "National code is required.")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "National code must be exactly 10 digits.")]
    public string NationalCode { get; set; }

    [Required(ErrorMessage = "PhoneNumber is required.")]
    [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Invalid phone number format.")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(50, ErrorMessage = "Name must be between {2} and {1} characters.", MinimumLength = 2)]
    [RegularExpression(@"^[a-zA-Z\s\u0600-\u06FF]+$", ErrorMessage = "Name can only contain letters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "LastName is required.")]
    [StringLength(50, ErrorMessage = "LastName must be between {2} and {1} characters.", MinimumLength = 2)]
    [RegularExpression(@"^[a-zA-Z\s\u0600-\u06FF]+$", ErrorMessage = "LastName can only contain letters.")]
    public string LastName { get; set; }
}

public class LoginDto
{
    [Required(ErrorMessage = "Username is required.")]
    [StringLength(30, ErrorMessage = "Username must be between {2} and {1} characters.", MinimumLength = 3)]
    [RegularExpression(@"^[a-zA-Z0-9._]+$",
        ErrorMessage = "Username can only contain letters, numbers, dots, and underscores.")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; }

    public bool RememberMe { get; set; }
}

public class ConfirmEmailDto
{
    [Required] public string UserId { get; set; }

    [Required]
    [RegularExpression(@"^\d{5}$", ErrorMessage = "Invalid verification code format.")]
    public string Code { get; set; }
}

public class PhoneNumberDto
{
    public string UserName { get; set; }
}

public class ConfirmPhoneDto
{
    public string UserName { get; set; }
    public string Code { get; set; }
}