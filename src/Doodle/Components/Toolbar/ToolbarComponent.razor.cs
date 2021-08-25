using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Doodle.Components.Toolbar
{

    public partial class ToolbarComponent : Shared.DoodleBaseComponent
    {

        #region Parameters
        public bool IsMenuOpen => DoodleDrawInteraction.ToolbarContent != Abstractions.Common.ToolbarContent.None;

        public Abstractions.Common.ToolbarContent ToolbarContent => DoodleDrawInteraction.ToolbarContent;
        #endregion

        #region Overrides
        public async Task CloseMenu()
        {
            await this.DoodleDrawInteraction.SetToolbarContent(Abstractions.Common.ToolbarContent.None);
        }

        protected override void OnInitialized()
        {
            this.DoodleDrawInteraction.OnToolbarContentChanged += (s, toolbarContent) => 
            {
                StateHasChanged();
            };
            base.OnInitialized();
        }
        #endregion



    }
}