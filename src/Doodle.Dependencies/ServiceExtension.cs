using Microsoft.Extensions.DependencyInjection;

namespace Doodle.Dependencies
{

  public static class ServiceExtension
  {

    public static IServiceCollection UseDoodleDependencies(this IServiceCollection serviceCollection)
    {
      serviceCollection.AddScoped<Abstractions.JsInterop.IJsInteropCanvas, Interops.JsInteropCanvas>();
      serviceCollection.AddScoped<Abstractions.JsInterop.IJsInteropCommon, Interops.JsInteropCommon>();

      return serviceCollection;
    }

  }

}