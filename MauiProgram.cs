using System.Reflection;
using CommunityToolkit.Maui;
using CoolGuys.Domain.Auth;
using CoolGuys.Domain.Post;
using CoolGuys.Domain.User;
using CoolGuys.Helpers;
using CoolGuys.UseCases;
using CoolGuys.UseCases._contracts;
using CoolGuys.UseCases.Auth;
using CoolGuys.UseCases.Post;
using CoolGuys.UseCases.User;
using CoolGuys.ViewModels;
using CoolGuys.Views;
using Flurl.Http;
using Flurl.Http.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using Mopups.Hosting;
using SkiaSharp.Views.Maui.Controls.Hosting;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;

namespace CoolGuys;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseSkiaSharp()
			.UseBarcodeReader()
			.ConfigureMopups()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		var config = GetConfig();
		builder.Configuration.AddConfiguration(config);


		//Helpers
		builder.Services.AddSingleton<IFlurlClientFactory, FlurlClientFactory>();
		builder.Services.AddSingleton<IFlurlClient>(x =>
		{
			return x.GetRequiredService<IFlurlClientFactory>().Get(builder.Configuration.GetSection("ApiUrl").Value);
		});
		
		//Auth feature
		builder.Services.AddScoped<IAuthService, AuthService>();
		builder.Services.AddScoped<Login>();
		builder.Services.AddScoped<Register>();
		
		//Post feature
		builder.Services.AddScoped<IPostService, PostService>();
		builder.Services.AddScoped<CreatePost>();
		builder.Services.AddScoped<AddPostImage>();
		builder.Services.AddScoped<ShowPost>();
		builder.Services.AddScoped<VotePost>();
		
		//User feature
		builder.Services.AddScoped<IUserService, UserService>();
		builder.Services.AddScoped<Avatar>();
		builder.Services.AddScoped<Details>();
		builder.Services.AddScoped<Friends>();
		
		//Views
		builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<MainPageViewModel>();
		builder.Services.AddTransient<PostPage>();
		builder.Services.AddTransient<PostPageViewModel>();
        builder.Services.AddTransient<SettingsPage>();
        builder.Services.AddTransient<SettingsPageViewModel>();

        var app = builder.Build();
		
		return app; 
	}
	
	static IConfiguration GetConfig()
	{
		var a = Assembly.GetExecutingAssembly();
		using var stream = a.GetManifestResourceStream("CoolGuys.appsettings.json");

		var config = new ConfigurationBuilder()
			.AddJsonStream(stream)
			.Build();
		return config;
	}
}
