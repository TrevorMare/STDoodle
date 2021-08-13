using System.Collections.Generic;
using Doodle.Abstractions.Config;
using Doodle.Abstractions.Models;
using Microsoft.AspNetCore.Components;

namespace Doodle.Components.Toolbar.BackgroundPicker
{

    public partial class BackgroundPickerComponent : Shared.DoodleBaseComponent
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
        protected override void InitConfigSettings(DoodleDrawConfig config)
        {
            if (config?.BackgroundConfig == null ) return;

            this.BackgroundSources = config.BackgroundConfig.BackgroundSources ?? new  List<Abstractions.Models.BackgroundData>();
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