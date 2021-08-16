using System.Collections.Generic;
using System.Threading.Tasks;
using Doodle.Abstractions.Config;
using Doodle.Abstractions.Models;
using Microsoft.AspNetCore.Components;

namespace Doodle.Components.Background
{

    public partial class BackgroundPickerComponent : Shared.DoodleBaseComponent
    {
       
        #region Properties

        [Parameter]
        public IEnumerable<BackgroundData> BackgroundSources { get; set; } = new List<Abstractions.Models.BackgroundData>();
        #endregion

        #region Methods
        protected override void InitConfigSettings(DoodleDrawConfig config)
        {
            if (config?.BackgroundConfig == null ) return;

            this.BackgroundSources = config.BackgroundConfig.BackgroundSources ?? new  List<Abstractions.Models.BackgroundData>();
        }

        private async Task ToggleSelectedBackground(Abstractions.Models.BackgroundData backgroundData)
        {
            if (await this.DoodleDrawInteraction.ContainsBackground(backgroundData))
            {
                await this.DoodleDrawInteraction.RemoveBackground(backgroundData);
            }
            else
            {
                await this.DoodleDrawInteraction.AddBackground(backgroundData);
            }
        }
        #endregion
        
    }

}