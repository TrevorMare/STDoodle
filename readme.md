# ST Doodle Draw 

## Welcome Message
Welcome to Sir Trixalot's Doodle Draw Component. It is a Blazor canvas drawing component with some flexibility on styling, theming and configuration.

This plugin was inspired by all the Angular npm packages that are so freely available for quick starts and not so much for Blazor. Additionally, since 
the .net5 release I have been burning to try it out. So this is my attempt at a contribution to the community and I really hope you find it usefull and saves you some time.

Check out the documentation and a live [demo](https://trevormare.github.io/STDoodle) here. 

## Important Notes
This plugin relies on .net5 and can be ported if you want. The whole idea for me regarding this project was to spend some time on the framework. It might also not have all the features of one of the existing packages but I'm pretty sure it can be extended to do what you want. Another thing I wanted to try was to use the minimum javascript to see how far I can push Blazor.

I would not suggest using this for mobile applications just yet, I have tested this on my Samsung Galaxy Tab A7 and the performance degrades the more you draw. 
This is also the reason why the JSInterop is not used for callbacks, but rather a hidden input value is set on the canvas element to retrieve the JSON
from the drawings.

# Roll your own
## Build Requirements
To build this project you need to have Satan's tool installed (NodeJs) as this is used for some of the dependencies that's included in the project.

# TLDR

## Installation
![Nuget](https://img.shields.io/nuget/v/STDoodle.Doodle?style=for-the-badge)
[![.NET](https://github.com/TrevorMare/STDoodle/actions/workflows/dotnet.yml/badge.svg)](https://github.com/TrevorMare/STDoodle/actions/workflows/dotnet.yml)

Download package and install [nuget](https://www.nuget.org/packages/STDoodle.Doodle/)

```shell 
    dotnet add package STDoodle.Doodle --version 1.0.7
```

## Usage
### Register the service in Startup.cs (Server Side)

```c
    using Doodle;
    ...
    ...
    public void ConfigureServices(IServiceCollection services)
    {
      ...
      services.UseDoodle((config) => 
      {
        config.DefaultStrokeColor = "#FF0000";
        config.DefaultStrokeSize = 2;
        ...
      });
      ...
    }
```
### Register the service in Startup.cs (WASM)

```c
    using Doodle;
    ...
    ...
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");

        builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
        builder.Services.UseDoodle((config) => 
        {
            config.DefaultStrokeColor = "#FF0000";
            config.DefaultStrokeSize = 2;
            ...
        });


        await builder.Build().RunAsync();
    }
```

### Styles

If this component will be used on mobile sites, add the following style to your stylesheet. This will help ensure that when the touch events
occur that the page is not accidentally refreshed and also so that the canvas can pick up the events straight away.

```css
body {
    overscroll-behavior: contain;
}
```

### Create a Doodle
  
The tour can be included in a separate Razor Component or on the page.

```html
    @using Doodle.Components

    <div style="height: 50vh;">
        <DoodleDraw @ref="_doodleDraw">
            
        </DoodleDraw>
    </div>

    @code {
    private DoodleDraw _doodleDraw;

    private string _jsonData;

    private async Task RenderTest() 
    {
        await _doodleDraw.ExportDoodleToImage();
    }

    private async Task SaveTest()
    {
        _jsonData = await _doodleDraw.SaveCurrentDrawState();
    }

    public async Task RestoreTest()
    {
        if (!string.IsNullOrEmpty(_jsonData))
        {
            await _doodleDraw.RestoreCurrentDrawState(_jsonData);
        }
    }

}
```

# Contributing
We all have some amazing ideas, but it's dependant on what the sand in the glass allows us. So if you have spare and wish to contribute to this project, feel free to do so.

1. Fork the Project
2. Create your Feature Branch (git checkout -b feature/WithKitchenSink)
3. Commit your Changes (git commit -m 'Add some bugs')
4. Push to the Branch (git push origin feature/WithKitchenSink)
5. Open a Pull Request

# Shout-outs
This project uses [html2canvas.js](https://html2canvas.hertzen.com/) to export the image. 

# Holler

Trevor Maré - [trevorm336@gmail.com](mailto:trevorm336@gmail.com)  
Project Link - [STDoodle](https://github.com/TrevorMare/STDoodle)

Buy me a beer
[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/donate?hosted_button_id=JTM723EPNE5N6)

# License
Distributed under the WTFPL License. See [LICENSE](http://www.wtfpl.net/) for more information.

# Stuff I learned
1. The built in JS Interopt is fast enough when using a desktop, but should probably not be used in this way for mobile browsers.




