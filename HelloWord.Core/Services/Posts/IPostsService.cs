using HelloWord.Core.Ioc;
using HelloWord.Messages.Requests.Posts;

namespace HelloWord.Core.Services.Posts;

public interface IPostsService: IScopedDependency
{
    Task<GetPostsListResponse> GetPostsListAsync(GetPostsListRequest request, CancellationToken cancellationToken);
}