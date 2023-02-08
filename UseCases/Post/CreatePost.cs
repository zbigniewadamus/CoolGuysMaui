using CoolGuys.UseCases._contracts;

namespace CoolGuys.UseCases.Post;

public class CreatePost
{
    private readonly IPostService postService;

    public CreatePost(IPostService postService)
    {
        this.postService = postService;
    }

    public Task<int> Exec(PostDto data)
    {
        return postService.CreatePost(data);
    }
}