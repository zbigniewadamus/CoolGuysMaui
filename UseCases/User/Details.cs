using CoolGuys.UseCases._contracts;

namespace CoolGuys.UseCases.User;

public class Details
{
    private readonly IUserService userService;

    public Details(IUserService userService)
    {
        this.userService = userService;
    }

    public Task<_contracts.User> Get()
    {
        return userService.GetCurrent();
    }

    public Task Add(UserDetailsDto data)
    {
        return userService.AddDetails(data);
    }
}