using CoolGuys.Helpers;
using CoolGuys.UseCases;
using CoolGuys.UseCases._contracts;
using Flurl.Http;

namespace CoolGuys.Domain.Auth;

public class AuthService : IAuthService
{
    private readonly IFlurlClient client;

    public AuthService(IFlurlClient client)
    {
        this.client = client;
    }

    public async Task Login(LoginDto data)
    {
        var response = await RequestHelper.HandleRequest(
            action: async () =>
            {
                var result = await client.Request("auth", "login")
                    .PostJsonAsync(data)
                    .ReceiveJson<ResponseDto>();
                return result;
            },
            unexpectedError: ex => throw ex);
        if (string.IsNullOrEmpty(response.message)) throw new Exception("Login failed unexpectedly");
        Preferences.Set("token", response.message);
    }

    public async Task Register(RegisterDto data)
    {
        var response = await RequestHelper.HandleRequest(
            action: async () =>
            {
                var result = await client.Request("auth", "register")
                    .PostJsonAsync(data)
                    .ReceiveJson<ResponseDto>();
                return result;
            },
            unexpectedError: ex => throw ex);
        if (string.IsNullOrEmpty(response?.message)) throw new Exception("Register failed unexpectedly");
        Preferences.Set("token", response.message);
    }
}