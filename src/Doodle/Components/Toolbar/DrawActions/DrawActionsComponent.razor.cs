using Microsoft.AspNetCore.Components;

namespace Doodle.Components.Toolbar.DrawActions
{

    public partial class DrawActionsComponent : Shared.DoodleBaseComponent
    {

        #region Members
        public bool UndoButtonEnabled => DoodleDrawInteraction.CanUndo;
        public bool RedoButtonEnabled => DoodleDrawInteraction.CanRedo;
        public bool ClearButtonEnabled => DoodleDrawInteraction.IsDirty;
        public bool SaveButtonEnabled => DoodleDrawInteraction.IsDirty;
        public bool ExportButtonEnabled => DoodleDrawInteraction.IsDirty;
        #endregion

        #region Properties
        [Parameter]
        public bool Show { get; set; } = true;

        [Parameter]
        public bool UndoButtonVisible { get; set; }

        [Parameter]
        public bool RedoButtonVisible { get; set; }

        [Parameter]
        public bool ClearButtonVisible { get; set; }

        [Parameter]
        public bool ClearHistoryOnClear { get; set; }

        [Parameter]
        public bool SaveButtonVisible { get; set; }

        [Parameter]
        public bool ExportButtonVisible { get; set; }

        #endregion

        #region Config Init
        protected override void InitConfigSettings(Abstractions.Config.DoodleDrawConfig config)
        {
            if (config == null || config.ToolbarConfig == null) return;
            
            this.Show = config.ToolbarConfig.ShowDrawActions;

            this.UndoButtonVisible = config.ToolbarConfig.ShowDrawActionsUndo;
            this.RedoButtonVisible = config.ToolbarConfig.ShowDrawActionsRedo;
            this.ClearButtonVisible = config.ToolbarConfig.ShowDrawActionsClear;
            this.ClearHistoryOnClear = config.ToolbarConfig.ClearHistoryOnClear;
            this.SaveButtonVisible = config.ToolbarConfig.ShowDrawActionsSave;
            this.ExportButtonVisible = config.ToolbarConfig.ShowDrawActionsExport;
        }
        #endregion


    }

}