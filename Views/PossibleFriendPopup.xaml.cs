using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using CoolGuys.UseCases._contracts;
using CoolGuys.UseCases.User;
using Mopups.Services;

namespace CoolGuys.Views;

public partial class PossibleFriendPopup 
{
    private readonly Friends friendsUseCase;
    public string Email { get; set; }
    public string FullName { get; set; }
    public string Avatar { get; set; }
    
    private readonly User user;

    public delegate void FriendAddedEventHandler(object sender, EventArgs e);
    public event FriendAddedEventHandler FriendAdded;
    public PossibleFriendPopup(User user, Friends friendsUseCase,FriendAddedEventHandler friendAddedEventHandler = null)
    {
        this.user = user;
        this.friendsUseCase = friendsUseCase;

        InitializeComponent();
        BindingContext = this;
        Email = user.Email;
        if (string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.FirstName))
            FullName = "Nie podano";
        else
            FullName = $"{user.FirstName}  {user.LastName}";
        Avatar = user.avatar ?? "logo_square_white.png";
        OnPropertyChanged(nameof(Email));
        OnPropertyChanged(nameof(FullName));
        OnPropertyChanged(nameof(Avatar));
        FriendAdded = friendAddedEventHandler;
    }

    private async void Add(object sender, EventArgs e)
    {
        try
        {
            await friendsUseCase.Add(user.Id);
            FriendAdded?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception err)
        {
            await MopupService.Instance.PopAllAsync();
            await Toast.Make(err.Message).Show();
        }
    }
}