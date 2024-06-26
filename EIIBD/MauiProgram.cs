﻿using Microsoft.Extensions.Logging;
using Camera.MAUI;

namespace EIIBD
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCameraView()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<App>();
            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddSingleton<CrearRetratoEnmarcado_UsandoLaCámara_Page>();

            builder.Services.AddSingleton<Marcos_Repository>();
            builder.Services.AddSingleton<SeleccionarMarco_Services>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
