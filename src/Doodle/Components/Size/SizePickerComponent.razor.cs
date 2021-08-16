using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Doodle.Components.Size
{

    public partial class SizePickerComponent : Shared.DoodleBaseComponent
    {

        #region Properties
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
        public double SelectedSize 
        { 
            get => this.DoodleDrawInteraction.StrokeWidth; 
            set
            {
                this.DoodleDrawInteraction.SetStrokeWidth(value);
            }
        }

        [Parameter]
        public Abstractions.Common.Orientation Orientation { get; set; }

        [Parameter]
        public IEnumerable<int> FavouriteSizes { get; set; }

        [Parameter]
        public string NumberInputClass { get; set; }

        [Parameter]
        public bool ShowCustomNumber { get; set; }
        #endregion

        #region Config Init
        protected override void InitConfigSettings(Abstractions.Config.DoodleDrawConfig config)
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
            this.ShowCustomNumber = config.SizePickerConfig.ShowCustomNumber;
        }
        #endregion

    }

}