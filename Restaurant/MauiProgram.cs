using Microsoft.Extensions.Logging;
using Restaurant.Pages;
using Restaurant.ConexionDatos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;

namespace Restaurant
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddHttpClient<ConexionDatos.IRestConexionDatos, ConexionDatos.RestConexionDatos>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<PlatosPage>();
            builder.Services.AddTransient<ListaPlatosPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

