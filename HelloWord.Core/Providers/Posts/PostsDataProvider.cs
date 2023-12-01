using System.Runtime.InteropServices.JavaScript;
using AutoMapper;
using HelloWord.Core.DbUp;
using HelloWord.Core.Domain.Posts;
using HelloWord.Messages.Requests.Posts;

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
    
    public async Task<IList<Post>> GetAllAsync(CancellationToken cancellationToken) => 
        await _repository.GetAllAsync<Post>(cancellationToken).ConfigureAwait(false);
    
}