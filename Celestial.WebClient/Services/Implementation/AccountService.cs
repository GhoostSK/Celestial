using Celestial.WebClient.Services.ApiClients.Account;
using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Interface;

namespace Celestial.WebClient.Services.Implementation;

public class AccountService(AccountApiClient client) : IAccountService
{
    public async Task<LoginResDto> LoginAsync(LoginReqDto request)
    {
        return await client.LoginAsync(request);
    }

    public async Task<ApiMessageResDto> RegisterAsync(RegisterReqDto request)
    {
        return await client.RegisterAsync(request);
    }

    public async Task<ApiMessageResDto> SendConfirmationCodeAsync(SendConfirmationCodeReqDto request)
    {
        return await client.SendConfirmationCodeAsync(request);
    }

    public async Task<ApiMessageResDto> ConfirmPhoneNumberAsync(ConfirmPhoneReqDto request)
    {
        return await client.ConfirmPhoneNumberAsync(request);
    }
}
