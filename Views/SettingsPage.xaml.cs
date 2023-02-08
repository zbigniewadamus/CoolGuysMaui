using CoolGuys.ViewModels;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using Button = Microsoft.Maui.Controls.Button;

namespace CoolGuys.Views;

public partial class SettingsPage: ContentPage
{
    public SettingsPage(SettingsPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        App.Current.On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
        
    }

    private async void AvatarAdd(object sender, EventArgs e)
    {
        var vm = BindingContext as SettingsPageViewModel;
        string path;
        var source = await DisplayActionSheet("Dodaj awatar", "Anuluj", null, "Galeria","Zrób zdjęcie" );
        switch (source)
        {
            case "Galeria":
                var file = await MediaPicker.PickPhotoAsync();
                path = file.FullPath;
                break;
            case "Zrób zdjęcie":
                var _file = await MediaPicker.CapturePhotoAsync();
                path = _file.FullPath;
                break;
            default:
                path = null;
                break;
        }

        if (path != null)
        {
            vm.AddAvatar(path);
        }
            
    }

    private async void EditClicked(object sender, EventArgs e)
    {
        var vm = BindingContext as SettingsPageViewModel;
        var btn = sender as Button;
        if (btn?.Text == "Edytuj")
        {
            btn.Text = "Zatwierdź";
            detailsStack.IsVisible = false;
            detailsStackEdit.IsVisible = true;
        }
        else
        {
            vm?.UpdateUser();
            btn.Text = "Edytuj";
            detailsStack.IsVisible = true;
            detailsStackEdit.IsVisible = false;
        }
        
        
        
    }
}