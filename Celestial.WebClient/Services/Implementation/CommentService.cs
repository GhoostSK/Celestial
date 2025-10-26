using Celestial.WebClient.Services.ApiClients.Comment;
using Celestial.WebClient.Services.DTO;
using Celestial.WebClient.Services.Interface;

namespace Celestial.WebClient.Services.Implementation;

public class CommentService(CommentApiClient client) : ICommentService
{
    public async Task<GetCommentsBySlugResDto> GetCommentsBySlugAsync(GetCommentsBySlugReqDto request)
    {
        return await client.GetCommentsBySlugAsync(request);
    }

    public async Task<AddCommentResDto> AddCommentAsync(AddCommentReqDto request)
    {
        return await client.AddCommentAsync(request);
    }
}