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
                serviceCollection.AddScoped<Abstractions.Config.DoodleDrawConfig>((s) => new Abstractions.Config.DoodleDrawConfig());
            }
            else
            {
                var configValue = new Abstractions.Config.DoodleDrawConfig();
                config.Invoke(configValue);
                serviceCollection.AddScoped<Abstractions.Config.DoodleDrawConfig>((s) => configValue);
            }

            serviceCollection.AddTransient<Abstractions.JsInterop.IJsInteropCanvas, Interops.JsInteropCanvas>();
            serviceCollection.AddScoped<Abstractions.JsInterop.IJsInteropCommon, Interops.JsInteropCommon>();
            serviceCollection.AddScoped<Abstractions.JsInterop.IJsInteropBuffer, Interops.JsInteropBuffer>();
            serviceCollection.AddScoped<Abstractions.JsInterop.IJsInteropHtml2Canvas, Interops.JsInteropHtml2Canvas>();
            serviceCollection.AddTransient<Abstractions.JsInterop.IJsInteropDragDrop, Interops.JsInteropDragDrop>();

            return serviceCollection;
        }

    }

}