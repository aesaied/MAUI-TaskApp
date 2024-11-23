using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using TaskApp.Helpers;
using TaskApp.Pages;

namespace TaskApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("fa-solid-900.ttf", "faSolid");

                });

            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<ManageTask>();


            builder.Services.AddSingleton<ApiHelper,ApiHelper>();
            builder.Services.AddSingleton<AuthHelper, AuthHelper>();
            builder.Services.AddSingleton<TaskHelper, TaskHelper>();



#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
