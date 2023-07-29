using CommunityToolkit.Maui;
using MemoryMaui.Pages;
using MemoryMaui.Services;
using Microsoft.Extensions.Logging;

namespace MemoryMaui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.RegisterServices()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
	private static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
	{
		builder.Services.AddSingleton<IGameService, GameService>();
		builder.Services.AddSingleton<IPlayerService,  PlayerService>();
		builder.Services.AddTransient<GamePage>();
		builder.Services.AddTransient<MainPage>();
		
		return builder;
	}
}
