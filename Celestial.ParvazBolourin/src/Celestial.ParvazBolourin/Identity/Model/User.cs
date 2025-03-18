using Microsoft.AspNetCore.Identity;

namespace Celestial.ParvazBolourin.Api.Identity.Model;

public class User : IdentityUser
{
    public string NationalCode { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string PhoneNumberConfirmationCode { get; set; }
    public int FailedConfirmationAttempts { get; set; } = 0; // تعداد تلاش‌های ناموفق
    public DateTime? ConfirmationCodeRequestedAt { get; set; }
    public DateTime? LockedUntil { get; set; } // زمان قفل شدن در صورت تلاش زیاد
}