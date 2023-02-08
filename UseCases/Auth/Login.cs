using CoolGuys.UseCases._contracts;

namespace CoolGuys.UseCases.Auth;

public class Login
{
    private readonly IAuthService authService;

    public Login(IAuthService authService)
    {
        this.authService = authService;
    }

    public Task Exec(LoginDto data)
    {
        return authService.Login(data);
    }
}