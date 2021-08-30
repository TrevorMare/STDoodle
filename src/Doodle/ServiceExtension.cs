using System;
using Microsoft.Extensions.DependencyInjection;
namespace Doodle
{

    public static class ServiceExtension
    {

        public static IServiceCollection UseDoodle(this IServiceCollection serviceCollection, Action<Abstractions.Config.DoodleDrawConfig> config = default)
        {

            if (config == null)
            {
                serviceCollection.AddScoped<Abstractions.Config.DoodleDrawConfig>((s) => { 
                    return new Abstractions.Config.DoodleDrawConfig()
                    {
                        Theme = new Themes.Default()
                    };
                });
            }
            else
            {
                var configValue = new Abstractions.Config.DoodleDrawConfig();
                config.Invoke(configValue);
                if (configValue.Theme == null)
                {
                    configValue.Theme = new Themes.Default();
                }
                serviceCollection.AddScoped<Abstractions.Config.DoodleDrawConfig>((s) => configValue);
            }

            serviceCollection.AddTransient<Abstractions.JsInterop.IJsInteropCanvas, Interops.JsInteropCanvas>();
            serviceCollection.AddScoped<Abstractions.JsInterop.IJsInteropCommon, Interops.JsInteropCommon>();
            serviceCollection.AddScoped<Abstractions.JsInterop.IJsInteropBuffer, Interops.JsInteropBuffer>();
            serviceCollection.AddScoped<Abstractions.JsInterop.IJsInteropHtml2Canvas, Interops.JsInteropHtml2Canvas>();

            serviceCollection.AddTransient<Abstractions.JsInterop.IJsInteropDragDrop, Interops.JsInteropDragDrop>();
            serviceCollection.AddTransient<Abstractions.Interfaces.IDoodleDrawInteraction, DoodleDrawInteraction>();
            serviceCollection.AddTransient<Abstractions.Interfaces.IDoodleExportHandler, Helpers.DoodleExportHandler>();
            serviceCollection.AddTransient<Abstractions.Interfaces.IDoodleSaveHandler, Helpers.DoodleSaveHandler>();
            serviceCollection.AddTransient<Abstractions.Interfaces.IDoodleStateManager, State.DoodleStateManager>();

            return serviceCollection;
        }

    }

}