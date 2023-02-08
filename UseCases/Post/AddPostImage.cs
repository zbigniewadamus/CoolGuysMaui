using CoolGuys.UseCases._contracts;

namespace CoolGuys.UseCases.Post;

public class AddPostImage
{
    private readonly IPostService postService;

    public AddPostImage(IPostService postService)
    {
        this.postService = postService;
    }

    public Task Exec(string path, int postId)
    {
        return postService.AddPostImage(path, postId);
    }
}