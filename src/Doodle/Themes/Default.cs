namespace Doodle.Themes
{

    public class Default : Abstractions.Interfaces.ITheme
    {

        public string ToolbarWrapperClass { get; set; } = "doodle-toolbar-wrapper";

        public string ToolbarHeaderClass { get; set; }  = "";

        public string ToolbarContentWrapperClass { get; set; }  = "doodle-toolbar-content-wrapper";

        public string ToolbarContentClass { get; set; }  = "";

        public string ToolbarMenuButtonClass { get; set; } = "doodle-btn-round";

        public string ToolbarMenuButtonsContainerClass { get; set; } = "";

        public string ToolbarMenuButtonsClass { get; set; } = "doodle-toolbar-buttons";

        public string ToolbarContentPanelsClass { get; set; } = "";

        public string ToolbarContentPanelClass { get; set; } = "";
    
        public string ToolbarContentPanelHeaderClass { get; set; } = "";

        public string ToolbarContentPanelHeaderTextClass { get; set; } = "doodle-toolbar-panel-header-text";  

        public string ToolbarContentPanelContentClass { get; set; } = "";  

        public string ToolbarContentPanelContentWrapperClass { get; set; } = "";

        public string ToolbarContentPanelContentCloseClass { get; set; } = "";
    }

}