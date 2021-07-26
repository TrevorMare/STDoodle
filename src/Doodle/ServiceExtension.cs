using Doodle.Dependencies;
using Microsoft.Extensions.DependencyInjection;

namespace Doodle
{

    public static class ServiceExtension
    {

        public static IServiceCollection UseDoodle(this IServiceCollection serviceCollection)
        {
            serviceCollection.UseDoodleDependencies();

            return serviceCollection;
        }

    }

}