using AutoMapper;
using HelloWord.Core.Services.Posts;
using HelloWord.Messages.Requests.Posts;
using Mediator.Net.Context;
using Mediator.Net.Contracts;

namespace HelloWord.Core.Handlers.RequestHandlers.Posts
{
    public class PostsRequestHandler: IRequestHandler<GetPostsListRequest,GetPostsListResponse>
    {

        private readonly IPostsService _service;
        private readonly IMapper _mapper;

        public PostsRequestHandler(IPostsService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<GetPostsListResponse> Handle(IReceiveContext<GetPostsListRequest> context, CancellationToken cancellationToken)
        {
            return await _service.GetPostsListAsync(context.Message, cancellationToken).ConfigureAwait(false);
        }
    

    }
}