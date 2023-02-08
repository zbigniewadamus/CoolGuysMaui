using CoolGuys.Helpers;
using CoolGuys.UseCases._contracts;
using CoolGuys.UseCases.User;
using Flurl.Http;

namespace CoolGuys.Domain.User;

public class UserService: IUserService
{
    private readonly IFlurlClient client;

    public UserService(IFlurlClient client)
    {
        this.client = client;
    }

    public Task SaveAvatar(string path)
    {
        return RequestHelper.HandleRequest(
            action: async () =>
            {
                var x =await client.Request("api", "user", "avatar")
                    .WithOAuthBearerToken(Preferences.Get("token", ""))
                    .PostMultipartAsync(mp=> mp.AddFile("image",path));
            },
            unexpectedError: ex =>
            {
                throw ex;
            });
    }

    public Task<UseCases._contracts.User> GetCurrent()
    {
        return RequestHelper.HandleRequest(
            action: async () =>
            {
                var response = await client.Request("api", "user", "me")
                    .WithOAuthBearerToken(Preferences.Get("token", ""))
                    .GetJsonAsync<ResponseDto<UseCases._contracts.User>>();
                return response.data;
            },
            unexpectedError: ex => throw ex);
    }

    public Task AddDetails(UserDetailsDto data)
    {
        return RequestHelper.HandleRequest(
            action: async () =>
            {
                await client.Request("api", "user", "me")
                    .WithOAuthBearerToken(Preferences.Get("token", ""))
                    .PutJsonAsync(data);
            },
            unexpectedError: ex => throw ex);
    }

    public Task<List<UseCases._contracts.User>> GetFriends()
    {
        return RequestHelper.HandleRequest(
            action: async () =>
            {
                var response = await client.Request("api", "user", "friends")
                    .WithOAuthBearerToken(Preferences.Get("token", ""))
                    .GetJsonAsync<ResponseDto<List<UseCases._contracts.User>>>();
                return response.data;
            },
            unexpectedError: ex => throw ex);
    }

    public Task AddFriend(int friendId)
    {
        return RequestHelper.HandleRequest(
            action: async () =>
            {
                await client.Request("api", "user", "friends", friendId)
                    .WithOAuthBearerToken(Preferences.Get("token", ""))
                    .PostAsync();
            },
            unexpectedError: ex => throw ex);
    }

    public Task DeleteFriend(int friendId)
    {
        return RequestHelper.HandleRequest(
            action: async () =>
            {
                await client.Request("api", "user", "friends", friendId)
                    .WithOAuthBearerToken(Preferences.Get("token", ""))
                    .DeleteAsync();
            },
            unexpectedError: ex => throw ex);
    }

    public Task<int> Search(string email)
    {
        return RequestHelper.HandleRequest(
            action: async () =>
            {
                var response = await client.Request("api", "user", "search")
                    .SetQueryParam("Email",email)
                    .WithOAuthBearerToken(Preferences.Get("token", ""))
                    .GetJsonAsync<ResponseDto>();
                return int.Parse(response.message); 
            },
            unexpectedError: ex => throw ex);
    }
}