using CoolGuys.UseCases._contracts;

namespace CoolGuys.UseCases.Post;

public class VotePost
{
    private readonly IPostService postService;

    public VotePost(IPostService postService)
    {
        this.postService = postService;
    }

    public Task Exec(int postId, bool positive)
    {
        return postService.Vote(postId, positive);
    }
}