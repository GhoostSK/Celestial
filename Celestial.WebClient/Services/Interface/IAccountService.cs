using Celestial.WebClient.Services.DTO;

namespace Celestial.WebClient.Services.Interface;

public interface IAccountService
{
    public Task<LoginResDto> LoginAsync(LoginReqDto request);
    public Task<ApiMessageResDto> RegisterAsync(RegisterReqDto request);
    public Task<ApiMessageResDto> SendConfirmationCodeAsync(SendConfirmationCodeReqDto request);
    public Task<ApiMessageResDto> ConfirmPhoneNumberAsync(ConfirmPhoneReqDto request);
}
