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

namespace STDoodle.DemoWASM
{
  public class Program
  {
    public static async Task Main(string[] args)
    {
      var builder = WebAssemblyHostBuilder.CreateDefault(args);
      builder.RootComponents.Add<App>("#app");

      builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
      builder.Services.UseDoodle((config) => 
      {
        config.DefaultStrokeColor = "#FF0000";
        config.DefaultStrokeSize = 2;
        config.CanvasConfig.UpdateResolution = 2;
        
        config.BackgroundConfig.BackgroundSources = new List<Doodle.Abstractions.Models.BackgroundData>()
        {
          new Doodle.Abstractions.Models.BackgroundData() { Name = "Car Back", BackgroundSourceType = Doodle.Abstractions.Common.BackgroundSourceType.Url, DataSource = "./img/car-back.png" },
          new Doodle.Abstractions.Models.BackgroundData() { Name = "Car Front", BackgroundSourceType = Doodle.Abstractions.Common.BackgroundSourceType.Url, DataSource = "./img/car-front.png" },
          new Doodle.Abstractions.Models.BackgroundData() { Name = "Car Side", BackgroundSourceType = Doodle.Abstractions.Common.BackgroundSourceType.Url, DataSource = "./img/car-side.png" },
          new Doodle.Abstractions.Models.BackgroundData() { Name = "Car Top", BackgroundSourceType = Doodle.Abstractions.Common.BackgroundSourceType.Url, DataSource = "./img/car-top.png" },
        };

        config.CanvasConfig.ResizableImages = new List<Doodle.Abstractions.Models.ResizableImageSource>()
        {
          new Doodle.Abstractions.Models.ResizableImageSource() { Name = "Signature", DataSource = "https://www.vhv.rs/dpng/d/511-5110816_transparent-fake-signature-png-calligraphy-png-download.png" },
          new Doodle.Abstractions.Models.ResizableImageSource() { Name = "Inspection Passed", DataSource = "./img/inspection-passed.png" },
          new Doodle.Abstractions.Models.ResizableImageSource() { Name = "Inspection Failed", DataSource = "./img/inspection-failed.png" }
        };

      });

      await builder.Build().RunAsync();
    }
  }
}
