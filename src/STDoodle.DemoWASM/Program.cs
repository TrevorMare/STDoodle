using Doodle;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GTour;

namespace STDoodle.DemoWASM
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      var builder = WebAssemblyHostBuilder.CreateDefault(args);
      builder.RootComponents.Add<App>("#app");

      builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
      
      builder.Services.UseGTour();
      
      builder.Services.UseDoodle((config) => 
      {
        config.DefaultStrokeColor = "#FF0000";
        config.DefaultStrokeSize = 2;
        config.CanvasConfig.UpdateResolution = 2;
        
        config.BackgroundConfig.BackgroundSources = new List<Doodle.Abstractions.Models.BackgroundData>()
        {
          new Doodle.Abstractions.Models.BackgroundData() { Name = "Car Back", BackgroundSourceType = Doodle.Abstractions.Common.BackgroundSourceType.Url, DataSource = "./_content/STDoodle.DemoComponents/img/car-back.png" },
          new Doodle.Abstractions.Models.BackgroundData() { Name = "Car Front", BackgroundSourceType = Doodle.Abstractions.Common.BackgroundSourceType.Url, DataSource = "./_content/STDoodle.DemoComponents/img/car-front.png" },
          new Doodle.Abstractions.Models.BackgroundData() { Name = "Car Side", BackgroundSourceType = Doodle.Abstractions.Common.BackgroundSourceType.Url, DataSource = "./_content/STDoodle.DemoComponents/img/car-side.png" },
          new Doodle.Abstractions.Models.BackgroundData() { Name = "Car Top", BackgroundSourceType = Doodle.Abstractions.Common.BackgroundSourceType.Url, DataSource = "./_content/STDoodle.DemoComponents/img/car-top.png" },
        };

        config.CanvasConfig.ResizableImages = new List<Doodle.Abstractions.Models.ResizableImageSource>()
        {
          new Doodle.Abstractions.Models.ResizableImageSource() { Name = "Signature", DataSource = "https://upload.wikimedia.org/wikipedia/commons/f/f8/Arnaldo_Tamayo_Signature.svg" },
          new Doodle.Abstractions.Models.ResizableImageSource() { Name = "Inspection Passed", DataSource = "./_content/STDoodle.DemoComponents/img/inspection-passed.svg" },
          new Doodle.Abstractions.Models.ResizableImageSource() { Name = "Inspection Failed", DataSource = "./_content/STDoodle.DemoComponents/img/inspection-failed.svg" }
        };

      });

      await builder.Build().RunAsync();
    }
  }
}
