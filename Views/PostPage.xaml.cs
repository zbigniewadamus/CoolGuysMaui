using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoolGuys.ViewModels;
using Mopups.Services;

namespace CoolGuys.Views;

public partial class PostPage : ContentPage
{
    public PostPage(PostPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
    async void ThumbPlusClicked(object sender, EventArgs e)
    {
        Animate(sender);
    }
    
    async void ThumbMinusClicked(object sender, EventArgs e)
    {
        Animate(sender);
    }

    async void Animate(object sender)
    {
        var btn = sender as ImageButton;
        var startY = btn.TranslationY;
        await btn.TranslateTo(0, startY - 5,150).ConfigureAwait(false);
        await btn.ScaleTo(0.7, 100, Easing.Linear);
        await btn.RotateTo(30, 250).ConfigureAwait(false);
        await btn.ScaleTo(1.2, 350, Easing.Linear);
        await btn.RotateTo(-30, 200).ConfigureAwait(false);
        await btn.TranslateTo(0, startY, 150).ConfigureAwait(false);
        await btn.RotateTo(0, 100).ConfigureAwait(false);
        await btn.ScaleTo(1, 200, Easing.Linear);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (Accelerometer.Default.IsSupported)
        {
            if (!Accelerometer.Default.IsMonitoring)
            {
                // Turn on compass
                Accelerometer.Default.ShakeDetected += ShakeDetected;
                Accelerometer.Default.Start(SensorSpeed.Game);
            }
        }

    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (Accelerometer.Default.IsSupported)
        {
            if (!Accelerometer.Default.IsMonitoring)
            {
                // Turn off compass
                Accelerometer.Default.Stop();
                Accelerometer.Default.ShakeDetected -= ShakeDetected;
            }
        }
    }

    private void ShakeDetected(object sender, EventArgs e)
    {
        (BindingContext as PostPageViewModel).LoadPostsCommand.Execute(null);
    }
}