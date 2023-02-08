using CoolGuys.UseCases._contracts;

namespace CoolGuys.UseCases.Post;

public class ShowPost
{
    private readonly IPostService postService;

    public ShowPost(IPostService postService)
    {
        this.postService = postService;
    }

    public Task<List<_contracts.Post>> Exec(int? page)
    {
        return postService.ShowAll(page);
    }
}