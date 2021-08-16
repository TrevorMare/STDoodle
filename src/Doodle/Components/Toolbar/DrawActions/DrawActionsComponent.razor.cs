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
        public bool Visible { get; set; } = true;

        [Parameter]
        public string WrapperClass { get; set; }

        [Parameter]
        public Abstractions.Common.Orientation Orientation { get; set; }

        [Parameter]
        public bool UndoButtonVisible { get; set; }

        [Parameter]
        public string UndoButtonClass { get; set; }

        [Parameter]
        public bool RedoButtonVisible { get; set; }

        [Parameter]
        public string RedoButtonClass { get; set; }

        [Parameter]
        public bool ClearButtonVisible { get; set; }

        [Parameter]
        public string ClearButtonClass { get; set; }

        [Parameter]
        public bool ClearHistoryOnClear { get; set; }

        [Parameter]
        public bool SaveButtonVisible { get; set; }

        [Parameter]
        public string SaveButtonClass { get; set; }

        [Parameter]
        public bool ExportButtonVisible { get; set; }

        [Parameter]
        public string ExportButtonClass { get; set; }
        #endregion

        #region Config Init
        protected override void InitConfigSettings(Abstractions.Config.DoodleDrawConfig config)
        {
            if (config == null || config.ToolbarConfig == null) return;

            this.Orientation = config.ToolbarConfig.Orientation;
            this.WrapperClass = config.ToolbarConfig.WrapperClass;
            this.Visible = config.ToolbarConfig.Visible;

            this.UndoButtonVisible = config.ToolbarConfig.UndoButtonVisible;
            this.UndoButtonClass = config.ToolbarConfig.UndoButtonClass;

            this.RedoButtonVisible = config.ToolbarConfig.RedoButtonVisible;
            this.RedoButtonClass = config.ToolbarConfig.RedoButtonClass;
 
            this.ClearButtonVisible = config.ToolbarConfig.ClearButtonVisible;
            this.ClearButtonClass = config.ToolbarConfig.ClearButtonClass;
            this.ClearHistoryOnClear = config.ToolbarConfig.ClearHistoryOnClear;

            this.SaveButtonVisible = config.ToolbarConfig.SaveButtonVisible;
            this.SaveButtonClass = config.ToolbarConfig.SaveButtonClass;

            this.ExportButtonVisible = config.ToolbarConfig.ExportButtonVisible;
            this.ExportButtonClass = config.ToolbarConfig.ExportButtonClass;
        }
        #endregion


    }

}