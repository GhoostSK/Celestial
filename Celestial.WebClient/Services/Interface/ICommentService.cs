using Celestial.WebClient.Services.DTO;

namespace Celestial.WebClient.Services.Interface;

public interface ICommentService
{
    public Task<GetCommentsBySlugResDto> GetCommentsBySlugAsync(GetCommentsBySlugReqDto request);
    public Task<AddCommentResDto> AddCommentAsync(AddCommentReqDto request);
}