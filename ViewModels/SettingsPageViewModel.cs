using CommunityToolkit.Mvvm.ComponentModel;
using CoolGuys.UseCases._contracts;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.Input;
using CoolGuys.UseCases.User;
using Mopups.Services;
using CoolGuys.Views;
using Newtonsoft.Json;

namespace CoolGuys.ViewModels
{
    public partial class SettingsPageViewModel : ObservableObject
    {
        private readonly Friends friendUseCase;
        private readonly Details details;
        private readonly Avatar avatar;

        [ObservableProperty] private string editFirstName;
        [ObservableProperty] private string editLastName;

        [ObservableProperty] private string email;

        [ObservableProperty] private User currentUser;

        [ObservableProperty] private List<User> friends = new List<User>();

        [ObservableProperty] private ImageSource qrSource = "";

        public SettingsPageViewModel(Friends friendUseCase, Details details, Avatar avatar)
        {
            this.friendUseCase = friendUseCase;
            this.details = details;
            this.avatar = avatar;
            GetData();
        }

        private async void GetData()
        {
            try
            {
                CurrentUser = await details.Get();
                Friends = await friendUseCase.GetAll() ?? new List<User>();
            }
            catch (Exception err)
            {
                await Toast.Make(err.Message).Show();
            }
        }

        [RelayCommand]
        async void CreateQr()
        {
            var data = JsonConvert.SerializeObject(CurrentUser);
            if (CurrentUser == null)
            {
                await Toast.Make("Błąd danych").Show();
                return;
            }

            await MopupService.Instance.PushAsync(new QrPopup(data));
        }

        public async void GetUser(User user)
        {
            await MopupService.Instance.PushAsync(new PossibleFriendPopup(user, friendUseCase, FriendAdded));
        }

        private async void FriendAdded(object sender, EventArgs e)
        {
            await MopupService.Instance.PopAllAsync();
            await Task.Delay(500);
            Friends = await friendUseCase.GetAll() ?? new List<User>();
        }

        [RelayCommand]
        async void LogOut()
        {
            Preferences.Clear();
            await Shell.Current.GoToAsync("///MainPage");
        }

        [RelayCommand]
        async void AddFriend()
        {
            await MopupService.Instance.PushAsync(new AddFriendPop(this));
        }

        [RelayCommand]
        async void SearchFriend()
        {
            try
            {
                int friendId = await friendUseCase.Search(Email);
                await friendUseCase.Add(friendId);
                await MopupService.Instance.PopAllAsync();
                await Task.Delay(500);
                await friendUseCase.GetAll();
            }
            catch (Exception err)
            {
                await Toast.Make(err.Message).Show();
            }
        }

        [RelayCommand]
        async void DeleteFriend(int id)
        {
            try
            {
                await friendUseCase.Delete(id);
                await Task.Delay(500);
                Friends = await friendUseCase.GetAll() ?? new List<User>();
            }
            catch (Exception err)
            {
                await Toast.Make(err.Message).Show();
            }
        }

        public async void AddAvatar(string path)
        {
            try
            {
                await avatar.Upload(path);
                await Task.Delay(1000);
                CurrentUser = new User
                {
                    Id = CurrentUser.Id,
                    FirstName = CurrentUser.FirstName,
                    LastName = CurrentUser.LastName,
                    Email = CurrentUser.Email,
                    avatar = CurrentUser.avatar
                };
            }
            catch (Exception err)
            {
                await Toast.Make(err.Message).Show();
            }
        }

        public async void UpdateUser()
        {
            try
            {
                var editedUser = new UserDetailsDto()
                {
                    FirstName = string.IsNullOrEmpty(EditFirstName) ? CurrentUser.FirstName : EditFirstName,
                    LastName = string.IsNullOrEmpty(EditLastName) ? CurrentUser.LastName : EditLastName
                };
                await details.Add(editedUser);
                CurrentUser = new User
                {
                    Email = CurrentUser.Email,
                    avatar = CurrentUser.avatar,
                    FirstName = editedUser.FirstName,
                    LastName = editedUser.LastName,
                    Id = CurrentUser.Id
                };
            }
            catch (Exception err)
            {
                await Toast.Make(err.Message).Show();
            }
        }
    }
}