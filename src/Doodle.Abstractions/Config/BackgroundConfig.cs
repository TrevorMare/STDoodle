using System.Collections.Generic;
using Doodle.Abstractions.Models;

namespace Doodle.Abstractions.Config
{

    public class BackgroundConfig
    {

        public IEnumerable<Models.BackgroundData> BackgroundSources { get; set; }

        public BackgroundConfig()
        {
            this.BackgroundSources = new List<Abstractions.Models.BackgroundData>()
            {
                new BackgroundData() { DataSource = "./_content/STDoodle/img/mono-kpresenter-kpr.svg" }, 
                new BackgroundData() { DataSource = "./_content/STDoodle/img/svg1.svg" }, 
                new BackgroundData() { DataSource = "./_content/STDoodle/img/svg2.svg" }, 
                new BackgroundData() { DataSource = "./_content/STDoodle/img/svg3.svg" }, 
                new BackgroundData() { DataSource = "./_content/STDoodle/img/svg4.svg", BackgroundClass = "test-svg-stroke" },
                new BackgroundData() { DataSource = "<svg xmlns='http://www.w3.org/2000/svg' height='100%' viewBox='0 0 24 24' width='100%' preserveAspectRatio='xMaxYMax' fill='#000000'><path d='M0 0h24v24H0V0z' fill='none'/><path d='M18.4 10.6C16.55 8.99 14.15 8 11.5 8c-4.65 0-8.58 3.03-9.96 7.22L3.9 16c1.05-3.19 4.05-5.5 7.6-5.5 1.95 0 3.73.72 5.12 1.88L13 16h9V7l-3.6 3.6z'/></svg>", BackgroundClass="test-svg-circle", BackgroundSourceType = Abstractions.Common.BackgroundSourceType.SVG} 
            };
        }
        
    }

}