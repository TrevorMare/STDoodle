using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Doodle.Components
{

    public partial class DoodleToolbar : ComponentBase
    {

        #region Members
        private Abstractions.Config.DoodleDrawConfig _config;

        private Abstractions.Config.DoodleDrawConfig _options;

        [Inject]
        private Abstractions.Config.DoodleDrawConfig Config 
        { 
            get => _config; 
            set
            {
                if (_config != value)
                {
                    _config = value;
                    InitConfigSettings(value);
                }
            } 
        }
        #endregion

        #region Properties
        [Parameter]
        public string DataAttributeName { get; set; }

        [Parameter]
        public bool Visible { get; set; } = true;

        [Parameter]
        public string WrapperClass { get; set; }

        public Abstractions.Common.Orientation Orientation { get; set; }

        [Parameter]
        public Abstractions.Config.DoodleDrawConfig Options 
        { 
            get => _options; 
            set 
            {
                if (_options != value)
                {
                    _options = value;
                    InitConfigSettings(value);
                }
            } 
        }

        [Parameter]
        public bool UndoButtonVisible { get; set; }

        [Parameter]
        public bool UndoButtonEnabled { get; set; } = false;

        [Parameter]
        public string UndoButtonClass { get; set; }

        [Parameter]
        public string UndoButtonText { get; set; } = "Undo";

        [Parameter]
        public RenderFragment UndoButtonContent { get; set; }

        [Parameter]
        public EventCallback UndoButtonClick { get; set; }

        [Parameter]
        public bool RedoButtonVisible { get; set; }

        [Parameter]
        public bool RedoButtonEnabled { get; set; } = false;

        [Parameter]
        public string RedoButtonClass { get; set; }

        [Parameter]
        public string RedoButtonText { get; set; } = "Redo";

        [Parameter]
        public RenderFragment RedoButtonContent { get; set; }

        [Parameter]
        public EventCallback RedoButtonClick { get; set; }

        [Parameter]
        public bool ClearButtonVisible { get; set; }

        [Parameter]
        public bool ClearButtonEnabled { get; set; } = false;

        [Parameter]
        public string ClearButtonClass { get; set; }

        [Parameter]
        public string ClearButtonText { get; set; } = "Clear";

        [Parameter]
        public RenderFragment ClearButtonContent { get; set; }

        [Parameter]
        public EventCallback<bool> ClearButtonClick { get; set; }

        [Parameter]
        public bool ClearHistoryOnClear { get; set; }

        [Parameter]
        public bool SaveButtonVisible { get; set; }

        [Parameter]
        public bool SaveButtonEnabled { get; set; } = false;

        [Parameter]
        public string SaveButtonClass { get; set; }

        [Parameter]
        public string SaveButtonText { get; set; } = "Save";

        [Parameter]
        public RenderFragment SaveButtonContent { get; set; }

        [Parameter]
        public EventCallback SaveButtonClick { get; set; }

        [Parameter]
        public bool ExportButtonVisible { get; set; }

        [Parameter]
        public bool ExportButtonEnabled { get; set; } = false;

        [Parameter]
        public string ExportButtonClass { get; set; }

        [Parameter]
        public string ExportButtonText { get; set; } = "Export";

        [Parameter]
        public RenderFragment ExportButtonContent { get; set; }

        [Parameter]
        public EventCallback ExportButtonClick { get; set; }
        #endregion

        #region Config Init
        private void InitConfigSettings(Abstractions.Config.DoodleDrawConfig config)
        {
            if (config == null) return;

            this.Orientation = config.ToolbarConfig?.Orientation ?? Abstractions.Common.Orientation.Vertical;
            this.WrapperClass = config.ToolbarConfig?.WrapperClass;
            this.Visible = config.ToolbarConfig?.Visible ?? true;

            this.UndoButtonVisible = config.ToolbarConfig?.UndoButtonVisible ?? true;
            this.UndoButtonClass = config.ToolbarConfig?.UndoButtonClass;
            this.UndoButtonText = config.ToolbarConfig?.UndoButtonText;

            this.RedoButtonVisible = config.ToolbarConfig?.RedoButtonVisible ?? true;
            this.RedoButtonClass = config.ToolbarConfig?.RedoButtonClass;
            this.RedoButtonText = config.ToolbarConfig?.RedoButtonText;

            this.ClearButtonVisible = config.ToolbarConfig?.ClearButtonVisible ?? true;
            this.ClearButtonClass = config.ToolbarConfig?.ClearButtonClass;
            this.ClearButtonText = config.ToolbarConfig?.ClearButtonText;
            this.ClearHistoryOnClear = config.ToolbarConfig?.ClearHistoryOnClear ?? false;

            this.SaveButtonVisible = config.ToolbarConfig?.SaveButtonVisible ?? true;
            this.SaveButtonText = config.ToolbarConfig?.SaveButtonText;
            this.SaveButtonClass = config.ToolbarConfig?.SaveButtonClass;

            this.ExportButtonVisible = config.ToolbarConfig?.ExportButtonVisible ?? true;
            this.ExportButtonText = config.ToolbarConfig?.ExportButtonText;
            this.ExportButtonClass = config.ToolbarConfig?.ExportButtonClass;
        }
        #endregion


    }

}