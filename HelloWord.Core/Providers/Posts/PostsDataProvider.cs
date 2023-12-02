using AutoMapper;
using HelloWord.Core.DbUp;

namespace HelloWord.Core.Providers.Posts;

public class PostsDataProvider: IPostsDataProvider
{
    private readonly IRepository _repository;
    private readonly IMapper _mapper;

    public PostsDataProvider(IRepository repository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = repository;
    }
    
    public async Task<IList<Domain.Posts.Posts>> GetAllAsync(CancellationToken cancellationToken) => 
        await _repository.GetAllAsync<Domain.Posts.Posts>(cancellationToken).ConfigureAwait(false);
    
}