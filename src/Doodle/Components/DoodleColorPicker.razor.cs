using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace Doodle.Components
{

    public partial class DoodleColorPicker : ComponentBase
    {

        #region "Parameters"
        [Parameter]
        public string E2ETestingName { get; set; }

        [Parameter]
        public string SelectedColor { get; set; }

        [Parameter]
        public EventCallback<string> SelectedColorChange { get; set; }

        [Parameter]
        public IEnumerable<string> FavouriteColors { get; set; } 

        [Parameter]
        public Abstractions.Common.Orientation Orientation { get; set; }

        private Abstractions.Config.DoodleDrawConfig _config;

        [Inject]
        private Abstractions.Config.DoodleDrawConfig Config 
        { 
            get => this._config;
            set 
            {
                this._config = value;
                this.InitConfigSettings();
            } 
        }

        [Inject]
        private ILogger<DoodleColorPicker> Logger { get; set; }

        [Parameter]
        public string WrapperClass { get; set; }

        [Parameter]
        public string FavouriteWrapperClass { get; set; }

        [Parameter]
        public string FavouriteOuterClass { get; set; }

        [Parameter]
        public string FavouriteInnerClass { get; set; }

        [Parameter]
        public string CustomWrapperClass { get; set; }
        #endregion

        #region Config Init
        private void InitConfigSettings()
        {
            this.SelectedColor = Config.ColorPickerConfig?.StartupColor ?? "#000000";
            this.Orientation = Config.ColorPickerConfig?.Orientation ?? Abstractions.Common.Orientation.Vertical;
            this.FavouriteColors = Config.ColorPickerConfig?.FavouriteColors ?? new List<string>();

            this.WrapperClass = Config.ColorPickerConfig?.WrapperClass;
            this.FavouriteWrapperClass = Config.ColorPickerConfig?.FavouriteWrapperClass;
            this.FavouriteOuterClass = Config.ColorPickerConfig?.FavouriteOuterClass;
            this.FavouriteInnerClass = Config.ColorPickerConfig?.FavouriteInnerClass;
            this.CustomWrapperClass = Config.ColorPickerConfig?.CustomWrapperClass;
        }
        #endregion

    }

}