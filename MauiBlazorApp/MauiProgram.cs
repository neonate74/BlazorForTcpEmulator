using Microsoft.Extensions.Logging;
using MauiBlazorApp.Services;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace MauiBlazorApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            using var stream = Assembly.GetExecutingAssembly()
                           .GetManifestResourceStream("MauiBlazorApp.appsettings.json");
            var config = new ConfigurationBuilder().AddJsonStream(stream).Build();
            builder.Configuration.AddConfiguration(config);

            builder.Services.AddTransient<MainPage>();

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

            NotificationService.ServerListenerModule.Publish();

            return builder.Build();
        }
    }
}