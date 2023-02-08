using CoolGuys.Views;

namespace CoolGuys;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute("settingsPage", typeof(SettingsPage));
		Routing.RegisterRoute("postPage", typeof(PostPage));
		Routing.RegisterRoute("mainPage", typeof(MainPage));
	}
}
