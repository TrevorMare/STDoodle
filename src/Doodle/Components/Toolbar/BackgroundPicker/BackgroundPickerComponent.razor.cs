using System.Collections.Generic;
using System.Threading.Tasks;
using Doodle.Abstractions.Config;
using Doodle.Abstractions.Models;
using Microsoft.AspNetCore.Components;

namespace Doodle.Components.Toolbar.BackgroundPicker
{

    public partial class BackgroundPickerComponent : Shared.DoodleBaseComponent 
    {

        #region Properties
        [Parameter]
        public bool Show { get; set; }

        [Parameter]
        public IEnumerable<BackgroundData> BackgroundSources { get; set; } = new List<Abstractions.Models.BackgroundData>();
        #endregion

        #region Methods 

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
        protected override void InitConfigSettings(DoodleDrawConfig config)
        {
            if (config.ToolbarConfig != null)
            {
                this.Show = config.ToolbarConfig.ShowBackgroundPicker;
            }

            if (config?.BackgroundConfig == null ) return;
            this.BackgroundSources = config.BackgroundConfig.BackgroundSources ?? new  List<Abstractions.Models.BackgroundData>();
        }
        #endregion
       
    }

}