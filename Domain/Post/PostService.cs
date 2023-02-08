using System.Net.Http.Headers;
using CoolGuys.Helpers;
using CoolGuys.UseCases._contracts;
using Flurl.Http;
using Newtonsoft.Json;

namespace CoolGuys.Domain.Post;

public class PostService:IPostService
{
    private readonly IFlurlClient client;

    public PostService(IFlurlClient client)
    {
        this.client = client;
    }

    public async Task<List<UseCases._contracts.Post>> ShowAll(int? page)
    {
        var response = await RequestHelper.HandleRequest(
            action: async () =>
            {
                var result = await client.Request("api", "post", "all")
                    .WithOAuthBearerToken(Preferences.Get("token",""))
                    .SetQueryParam("page",page)
                    .GetJsonAsync<ResponseDto<List<UseCases._contracts.Post>>>();
                return result;
            },
            unexpectedError: ex=> throw ex);
        var result = response?.data;
        return result ?? new List<UseCases._contracts.Post>();
    }

    public async Task Vote(int postId, bool positive)
    { 
        await RequestHelper.HandleRequest(
            action: async () =>
            {
                await client.Request("api", "post", "vote")
                    .SetQueryParam("postId", postId)
                    .SetQueryParam("positive", positive ? "true" : "false")
                    .WithOAuthBearerToken(Preferences.Get("token", ""))
                    .PutAsync();
            },
            unexpectedError: ex => throw ex);
        return;
    }

    public Task<int> CreatePost(PostDto data)
    {
        return RequestHelper.HandleRequest(
            action: async () =>
            {
                var response = await client.Request("api", "post", "create")
                    .WithOAuthBearerToken(Preferences.Get("token", ""))
                    .PostJsonAsync(data)
                    .ReceiveJson<ResponseDto>();
                if (int.TryParse(response.message, out int result))
                {
                    return result;
                }
                throw new Exception("Nie udało się skonwertować identyfikatora posta");
            },
            unexpectedError: ex => throw ex);
    }

    public async Task AddPostImage(string path, int postId)
    {
         await RequestHelper.HandleRequest(
            action: async () =>
            {
                var x =await client.Request("api", "post", "image", postId)
                    .WithOAuthBearerToken(Preferences.Get("token", ""))
                    .PostMultipartAsync(mp=> mp.AddFile("data",path));
            },
            unexpectedError: ex =>
            {
                throw ex;
            });
    }
}