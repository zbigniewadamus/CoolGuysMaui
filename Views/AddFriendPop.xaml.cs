using CoolGuys.ViewModels;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing.Net.Maui;
using CommunityToolkit.Maui.Alerts;
using Newtonsoft.Json;
using CoolGuys.UseCases._contracts;

namespace CoolGuys.Views;
public partial class AddFriendPop 
{
    public AddFriendPop(SettingsPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private async void OnScan(object sender, BarcodeDetectionEventArgs e)
    {
        if(e.Results[0].Format != BarcodeFormat.QrCode)
        {
            await Toast.Make("Podany kod nie jest kodem QR").Show();            
            return;
        }
        try
        {
            var user = JsonConvert.DeserializeObject<User>(e.Results[0].Value);
            qrScanner.IsDetecting = false;
            await MopupService.Instance.PopAllAsync();
            (BindingContext as SettingsPageViewModel).GetUser(user);
        }
        catch(Exception ex)
        {
            await Toast.Make("Nie udało się odczytać QR").Show();
        }
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        qrScanner.IsDetecting = true;
        qrScanner.IsEnabled = true;
        qrScanner.IsVisible = true;
    }
    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        qrScanner.IsDetecting = false;
        qrScanner.IsEnabled = false;
        qrScanner.IsVisible = false;
    }

    private void qrScanner_BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {

    }
}