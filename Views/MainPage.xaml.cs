using CoolGuys.ViewModels;

namespace CoolGuys.Views;


public partial class MainPage : ContentPage
{
	public MainPage(MainPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	public async void ShowDataForm(object sender, EventArgs e)
	{
		var btn = sender as Button;
		confirmPassword.IsVisible = btn.Text == "Zarejestruj się";
		submitButton.Text = btn.Text == "Zarejestruj się" ? "Zarejestruj" : "Zaloguj";
		submitButton.CommandParameter = btn.Text == "Zarejestruj się" ? "Zarejestruj" : "Zaloguj";
		DataForm.IsVisible= true;
		await DataForm.TranslateTo(DataForm.TranslationX, 0, 800, Easing.Linear);
		animationLaugh.IsVisible = false;
	}
	public async void HideDataForm(object sender, EventArgs e)
	{
        animationLaugh.IsVisible = true;
        await DataForm.TranslateTo(DataForm.TranslationX, 1000, 800, Easing.Linear);
        DataForm.IsVisible = false;
    }
}

