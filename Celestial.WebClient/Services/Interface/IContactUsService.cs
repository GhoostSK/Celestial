using Celestial.WebClient.Services.DTO;

namespace Celestial.WebClient.Services.Interface;

public interface IContactUsService
{
    public Task<AddContactUsResDto> AddContactUsAsync(AddContactUsReqDto request);
}
