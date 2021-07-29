using System;
using Doodle.Dependencies;
using Microsoft.Extensions.DependencyInjection;

namespace Doodle
{

    public static class ServiceExtension
    {

        public static IServiceCollection UseDoodle(this IServiceCollection serviceCollection, Action<Abstractions.Config.DoodleDrawConfig> config = default)
        {
            serviceCollection.UseDoodleDependencies();

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

            return serviceCollection;
        }

    }

}