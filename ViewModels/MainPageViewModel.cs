using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CoolGuys.UseCases;
using CoolGuys.UseCases._contracts;
using CoolGuys.UseCases.Auth;
using CoolGuys.Views;

namespace CoolGuys.ViewModels;

public partial class MainPageViewModel: ObservableObject
{
    [ObservableProperty] private string email;
    [ObservableProperty] private string password;
    [ObservableProperty] private string confirmPassword;
    
    
    private readonly Login login;
    private readonly Register register;

    public MainPageViewModel(Login login, Register register)
    {
        if(Preferences.ContainsKey("token"))
            if(!string.IsNullOrEmpty(Preferences.Get("token","")))
                Shell.Current.GoToAsync("///PostPage", true);
        this.login = login;
        this.register = register;
    }

    [RelayCommand]
    async void Submit(string param)
    {
        try
        {
            if(param == "Zaloguj")
                await login.Exec(new LoginDto { Email = Email, Password = Password });
            else
                await register.Exec(new RegisterDto
                { Email = Email, Password = Password, ConfirmPassword = ConfirmPassword });
            await Shell.Current.GoToAsync("///PostPage", true);
        }
        catch (Exception err)
        {
            await Toast.Make(err.Message).Show();
        }
    }
}