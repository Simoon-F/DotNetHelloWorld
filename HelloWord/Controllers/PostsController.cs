using HelloWord.Messages.Requests.Posts;
using Mediator.Net;
using Microsoft.AspNetCore.Mvc;

namespace HelloWord.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IMediator _mediator;

    public PostsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("posts")]
    public async Task<GetPostsListResponse> GetPostsListAsync([FromQuery] GetPostsListRequest request) {
        return await _mediator.RequestAsync<GetPostsListRequest, GetPostsListResponse>(request);
    }
}