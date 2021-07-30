using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace Doodle.Components
{

    public partial class DoodleColorPicker : ComponentBase
    {

        #region Members
        private string _selectedColor;
        private Abstractions.Config.DoodleDrawConfig _options;
        #endregion

        #region "Parameters"
        [Parameter]
        public string E2ETestingName { get; set; }

        [Parameter]
        public string SelectedColor 
        { 
            get => _selectedColor; 
            set 
            {
                if (_selectedColor != value)
                {
                    _selectedColor = value;
                    SelectedColorChanged.InvokeAsync(_selectedColor);
                }
            } 
        }

        [Parameter]
        public EventCallback<string> SelectedColorChanged { get; set; }

        [Parameter]
        public Abstractions.Config.DoodleDrawConfig Options 
        { 
            get => _options; 
            set
            {
                if (_options != value)
                {
                    _options = value;
                    this.InitConfigSettings(_options);
                }
            } 
        }

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
                this.InitConfigSettings(this._config);
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

        [Parameter]
        public string ColorInputClass { get; set; }

        [Parameter]
        public bool Visible { get; set; } = true;

        [Parameter]
        public bool ShowCustomColor { get; set; }
        #endregion

        #region Config Init
        private void InitConfigSettings(Abstractions.Config.DoodleDrawConfig config)
        {
            if (config == null) return;

            this.Orientation = config.ColorPickerConfig?.Orientation ?? Abstractions.Common.Orientation.Vertical;
            this.FavouriteColors = config.ColorPickerConfig?.FavouriteColors ?? new List<string>();

            this.WrapperClass = config.ColorPickerConfig?.WrapperClass;
            this.FavouriteWrapperClass = config.ColorPickerConfig?.FavouriteWrapperClass;
            this.FavouriteOuterClass = config.ColorPickerConfig?.FavouriteOuterClass;
            this.FavouriteInnerClass = config.ColorPickerConfig?.FavouriteInnerClass;
            this.CustomWrapperClass = config.ColorPickerConfig?.CustomWrapperClass;
            this.ColorInputClass = config.ColorPickerConfig?.ColorInputClass;
            this.ShowCustomColor = config.ColorPickerConfig?.ShowCustomColor ?? true;
            this.Visible = config.ColorPickerConfig?.Visible ?? true;
        }
        #endregion

    }

}