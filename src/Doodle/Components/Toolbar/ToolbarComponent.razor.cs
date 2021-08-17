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