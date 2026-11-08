using Celestial.WebClient.Services.ApiClients.Contact;
using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Interface;

namespace Celestial.WebClient.Services.Implementation;

public class ContactUsService(ContactUsApiClient client) : IContactUsService
{
    public async Task<AddContactUsResDto> AddContactUsAsync(AddContactUsReqDto request)
    {
        return await client.AddContactUsAsync(request);
    }
}
