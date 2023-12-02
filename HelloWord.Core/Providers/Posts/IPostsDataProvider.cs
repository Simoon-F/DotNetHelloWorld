using HelloWord.Core.Domain.Posts;
using HelloWord.Core.Ioc;

namespace HelloWord.Core.Providers.Posts;

public interface IPostsDataProvider: IScopedDependency
{
    Task<IList<Domain.Posts.Posts>> GetAllAsync(CancellationToken cancellationToken = default);

}