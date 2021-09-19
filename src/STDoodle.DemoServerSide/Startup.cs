using Doodle;
using GTour;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using STDoodle.DemoServerSide.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace STDoodle.DemoServerSide
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddRazorPages();
      services.AddServerSideBlazor();
      services.AddSingleton<WeatherForecastService>();

      services.UseGTour();

      services.UseDoodle((config) => 
      {
        config.DefaultStrokeColor = "#FF0000";
        config.DefaultStrokeSize = 2;

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
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
      }

      app.UseStaticFiles();

      app.UseRouting();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapBlazorHub();
        endpoints.MapFallbackToPage("/_Host");
      });
    }
  }
}
