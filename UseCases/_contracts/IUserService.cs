namespace CoolGuys.UseCases._contracts;

public interface IUserService
{
    Task SaveAvatar(string path);
    Task<User> GetCurrent();
    Task AddDetails(UserDetailsDto data);
    Task<List<User>> GetFriends();
    Task AddFriend(int friendId);
    Task DeleteFriend(int friendId);
    Task<int> Search(string email);
}