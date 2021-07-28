using Microsoft.Extensions.DependencyInjection;

namespace Doodle.Dependencies
{

  public static class ServiceExtension
  {

    public static IServiceCollection UseDoodleDependencies(this IServiceCollection serviceCollection)
    {
      serviceCollection.AddScoped<Abstractions.JsInterop.IJsInteropCanvas, Interops.JsInteropCanvas>();
      serviceCollection.AddScoped<Abstractions.JsInterop.IJsInteropCommon, Interops.JsInteropCommon>();

      serviceCollection.AddScoped<Abstractions.JsInterop.IJsInteropBuffer, Interops.JsInteropBuffer>();
      serviceCollection.AddScoped<Abstractions.JsInterop.IJsInteropHtml2Canvas, Interops.JsInteropHtml2Canvas>();

      return serviceCollection;
    }

  }

}