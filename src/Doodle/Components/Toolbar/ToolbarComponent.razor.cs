using System.Threading.Tasks;
using Doodle.Abstractions.Config;
using Microsoft.AspNetCore.Components;

namespace Doodle.Components.Toolbar
{

    public partial class ToolbarComponent : Shared.DoodleBaseComponent
    {

        #region "Properties"
        [Parameter]
        public bool Show { get; set; } = true;
        #endregion

        #region Members
        public bool IsMenuOpen => DoodleDrawInteraction.ToolbarContent != Abstractions.Common.ToolbarContent.None;

        public Abstractions.Common.ToolbarContent ToolbarContent => DoodleDrawInteraction.ToolbarContent;
        #endregion

        #region Overrides
        protected override void OnInitialized()
        {
            this.DoodleDrawInteraction.OnToolbarContentChanged += (s, toolbarContent) => 
            {
                StateHasChanged();
            };
            base.OnInitialized();
        }

        protected override void InitConfigSettings(DoodleDrawConfig config)
        {
            if (config.ToolbarConfig == null)
            {
                return;
            }
            this.Show = config.ToolbarConfig.ShowToolbar;
        }
        #endregion

        #region Methods
        public async Task CloseMenu()
        {
            await this.DoodleDrawInteraction.SetToolbarContent(Abstractions.Common.ToolbarContent.None);
        }
        #endregion



    }
}