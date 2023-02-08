using CoolGuys.UseCases._contracts;

namespace CoolGuys.UseCases.User;

public class Avatar
{
    private readonly IUserService userService;

    public Avatar(IUserService userService)
    {
        this.userService = userService;
    }

    public Task Upload(string image)
    {
        return userService.SaveAvatar(image);
    }
}