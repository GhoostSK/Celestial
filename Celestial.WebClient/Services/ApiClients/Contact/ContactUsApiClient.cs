using Celestial.WebClient.Services.ApiClients.Base;
using Celestial.WebClient.Services.DTO;

namespace Celestial.WebClient.Services.ApiClients.Contact;

public class ContactUsApiClient(HttpJsonClient httpClient)
{
    public async Task<AddContactUsResDto> AddContactUsAsync(AddContactUsReqDto request)
    {
        return await httpClient.PostAsync<AddContactUsReqDto, AddContactUsResDto>(
            $"/api/{request.Language}/ContactUs/AddContactUs",
            request);
    }
}
