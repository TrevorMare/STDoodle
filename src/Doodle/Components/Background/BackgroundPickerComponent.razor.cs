using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doodle.Abstractions.Config;
using Doodle.Abstractions.Models;
using Microsoft.AspNetCore.Components;

namespace Doodle.Components.Background
{

    public partial class BackgroundPickerComponent : Shared.DoodleBaseComponent
    {

        #region Members
        private List<BackgroundData> _selectedBackgrounds = new List<BackgroundData>();
        #endregion
       
        #region Properties

        [Parameter]
        public IEnumerable<BackgroundData> BackgroundSources { get; set; } = new List<Abstractions.Models.BackgroundData>();

        [Parameter]
        public EventCallback OnCloseMenu { get; set; }
        #endregion

        #region Methods
        protected override void OnInitialized()
        {
            this.DoodleDrawInteraction.DoodleStateManager.OnRestoreState += (s, e) => 
            {
                this._selectedBackgrounds = this.DoodleDrawInteraction.DoodleStateManager.SelectedBackgrounds.ToList();
                this.StateHasChanged();
            };
            base.OnInitialized();
        }

        protected override void InitConfigSettings(DoodleDrawConfig config)
        {
            if (config?.BackgroundConfig == null ) return;

            this.BackgroundSources = config.BackgroundConfig.BackgroundSources ?? new  List<Abstractions.Models.BackgroundData>();
        }

        private bool IsBackgroundSelected(Abstractions.Models.BackgroundData backgroundData)
        {
            if (backgroundData == null) return false;
            return this._selectedBackgrounds.Any(x => x.Id == backgroundData.Id);
        }

        private async Task ToggleSelectedBackground(Abstractions.Models.BackgroundData backgroundData)
        {
            if (this.IsBackgroundSelected(backgroundData)) 
            {
                this._selectedBackgrounds.Remove(this._selectedBackgrounds.First(x => x.Id == backgroundData.Id));
            }
            else
            {
                this._selectedBackgrounds.Add(backgroundData);
            }
            await this.DoodleDrawInteraction.DoodleStateManager.PushBackgroundState(new State.BackgroundState(this._selectedBackgrounds));
        }
        #endregion
        
    }

}