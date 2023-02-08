namespace CoolGuys.UseCases._contracts;

public interface IPostService
{
    Task<List<_contracts.Post>> ShowAll(int? page);
    Task Vote(int postId, bool positive);
    Task<int> CreatePost(PostDto data);
    Task AddPostImage(string path, int postId);
}