using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Doodle.Components
{

    public partial class DoodleSizePicker : ComponentBase
    {

        #region Members
        private int _selectedSize = 1;

        private Abstractions.Config.DoodleDrawConfig _config;

        private Abstractions.Config.DoodleDrawConfig _options;

        [Inject]
        private Abstractions.Config.DoodleDrawConfig Config 
        { 
            get => _config; 
            set
            {
                if (_config != value)
                {
                    _config = value;
                    InitConfigSettings(value);
                }
            } 
        }
        #endregion

        #region Properties
        [Parameter]
        public string DataAttributeName { get; set; }

        [Parameter]
        public bool Visible { get; set; } = true;

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
        public int SelectedSize 
        { 
            get => this._selectedSize; 
            set
            {
                if (this._selectedSize != value && value > 0)
                {
                    this._selectedSize = value;
                    this.SelectedSizeChanged.InvokeAsync(value);
                }
            }
        }

        [Parameter]
        public EventCallback<int> SelectedSizeChanged { get; set; }

        [Parameter]
        public Abstractions.Common.Orientation Orientation { get; set; }

        [Parameter]
        public IEnumerable<int> FavouriteSizes { get; set; }

        [Parameter]
        public Abstractions.Config.DoodleDrawConfig Options 
        { 
            get => _options; 
            set 
            {
                if (_options != value)
                {
                    _options = value;
                    InitConfigSettings(value);
                }
            } 
        }

        [Parameter]
        public string NumberInputClass { get; set; }

        [Parameter]
        public bool ShowCustomNumber { get; set; }
        #endregion

        #region Config Init
        private void InitConfigSettings(Abstractions.Config.DoodleDrawConfig config)
        {
            if (config == null || config.SizePickerConfig == null) return;

            this.Orientation = config.SizePickerConfig.Orientation;
            this.FavouriteSizes = config.SizePickerConfig.FavouriteSizes ?? new List<int>();

            this.WrapperClass = config.SizePickerConfig.WrapperClass;
            this.FavouriteWrapperClass = config.SizePickerConfig.FavouriteWrapperClass;
            this.FavouriteOuterClass = config.SizePickerConfig.FavouriteOuterClass;
            this.FavouriteInnerClass = config.SizePickerConfig.FavouriteInnerClass;
            this.CustomWrapperClass = config.SizePickerConfig.CustomWrapperClass;
            this.NumberInputClass = config.SizePickerConfig.NumberInputClass;
            this.Visible = config.SizePickerConfig.Visible;
            this.ShowCustomNumber = config.SizePickerConfig.ShowCustomNumber;
        }
        #endregion

    }

}