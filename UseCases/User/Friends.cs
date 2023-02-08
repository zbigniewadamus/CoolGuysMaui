using CoolGuys.UseCases._contracts;

namespace CoolGuys.UseCases.User;

public class Friends
{
    private readonly IUserService userService;

    public Friends(IUserService userService)
    {
        this.userService = userService;
    }

    public Task<List<_contracts.User>> GetAll()
    {
        return userService.GetFriends();
    }

    public Task Add(int friendId)
    {
        return userService.AddFriend(friendId);
    }
    
    public Task Delete(int friendId)
    {
        return userService.DeleteFriend(friendId);
    }

    public Task<int> Search(string email)
    {
        return userService.Search(email);
    }
}