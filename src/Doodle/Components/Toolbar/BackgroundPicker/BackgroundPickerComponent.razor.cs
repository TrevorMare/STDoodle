using System.Collections.Generic;
using Doodle.Abstractions.Models;
using Microsoft.AspNetCore.Components;

namespace Doodle.Components.Toolbar.BackgroundPicker
{

    public partial class BackgroundPickerComponent : ComponentBase
    {

        #region Properties
        [Parameter]
        public bool IsOpen { get; set; }

        [Parameter]
        public IEnumerable<BackgroundData> BackgroundSources { get; set; } = new List<Abstractions.Models.BackgroundData>();

        public List<BackgroundData> SelectedBackgrounds { get; private set; } = new List<Abstractions.Models.BackgroundData>();

        [Parameter]
        public EventCallback<List<BackgroundData>> SelectedBackgroundsChanged { get; set; }
        #endregion

        #region Methods
        protected override void OnInitialized()
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
            
            base.OnInitialized();
        }


        private void ToggleSelectedBackground(Abstractions.Models.BackgroundData backgroundData)
        {
            if (SelectedBackgrounds.Contains(backgroundData))
            {
                SelectedBackgrounds.Remove(backgroundData);
            }
            else
            {
                SelectedBackgrounds.Add(backgroundData);
            }
            this.SelectedBackgroundsChanged.InvokeAsync(SelectedBackgrounds);
        }
        #endregion
       
    }


}