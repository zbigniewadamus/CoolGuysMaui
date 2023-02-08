namespace CoolGuys.UseCases._contracts;

public interface IAuthService
{
    Task Login(LoginDto data);
    Task Register(RegisterDto data);
}