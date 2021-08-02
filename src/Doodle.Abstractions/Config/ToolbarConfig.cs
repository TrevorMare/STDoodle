using System.Collections.Generic;

namespace Doodle.Abstractions.Config
{

    public class ToolbarConfig
    {
        public Common.Orientation Orientation { get; set; } = Common.Orientation.Horizontal;

        public string WrapperClass { get; set; }
        
        public bool Visible { get; set; } = true;

        public bool UndoButtonVisible { get; set; } = true;

        public string UndoButtonClass { get; set; }

        public string UndoButtonText { get; set; } = "Undo";

        public bool RedoButtonVisible { get; set; } = true;

        public string RedoButtonClass { get; set; }

        public string RedoButtonText { get; set; } = "Redo";

        public bool ClearButtonVisible { get; set; } = true;

        public string ClearButtonClass { get; set; }

        public string ClearButtonText { get; set; } = "Clear";

        public bool ClearHistoryOnClear { get; set; }

        public bool SaveButtonVisible { get; set; } = true;

        public string SaveButtonClass { get; set; }

        public string SaveButtonText { get; set; } = "Save";

        public bool ExportButtonVisible { get; set; } = true;   

        public string ExportButtonClass { get; set; }

        public string ExportButtonText { get; set; } = "Export";
    }

}