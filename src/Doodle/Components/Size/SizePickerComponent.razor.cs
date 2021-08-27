using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Doodle.Components.Size
{

    public partial class SizePickerComponent : Shared.DoodleBaseComponent
    {

        #region Properties
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
        public IEnumerable<int> FavouriteSizes { get; set; }
        
        [Parameter]
        public bool ShowCustomNumber { get; set; }

        [Parameter]
        public EventCallback OnCloseMenu { get; set; }
        #endregion

        #region Config Init
        protected override void InitConfigSettings(Abstractions.Config.DoodleDrawConfig config)
        {
            if (config == null || config.SizePickerConfig == null) return;

            this.FavouriteSizes = config.SizePickerConfig.FavouriteSizes ?? new List<int>();
            this.ShowCustomNumber = config.SizePickerConfig.ShowCustomNumber;
        }
        #endregion

    }

}