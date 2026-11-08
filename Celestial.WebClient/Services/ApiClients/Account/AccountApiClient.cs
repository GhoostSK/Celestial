using Celestial.WebClient.Services.ApiClients.Base;
using Celestial.WebClient.Services.DTO;

namespace Celestial.WebClient.Services.ApiClients.Account;

public class AccountApiClient(HttpJsonClient httpClient)
{
    public async Task<LoginResDto> LoginAsync(LoginReqDto request)
    {
        return await httpClient.PostAsync<LoginReqDto, LoginResDto>(
            $"/api/{request.Language}/Account/Login",
            request);
    }

    public async Task<ApiMessageResDto> RegisterAsync(RegisterReqDto request)
    {
        return await httpClient.PostForMessageAsync(
            $"/api/{request.Language}/Account/Register",
            request);
    }

    public async Task<ApiMessageResDto> SendConfirmationCodeAsync(SendConfirmationCodeReqDto request)
    {
        return await httpClient.PostForMessageAsync(
            $"/api/{request.Language}/Account/SendConfirmationCode",
            request);
    }

    public async Task<ApiMessageResDto> ConfirmPhoneNumberAsync(ConfirmPhoneReqDto request)
    {
        return await httpClient.PostForMessageAsync(
            $"/api/{request.Language}/Account/ConfirmPhoneNumber",
            request);
    }
}
