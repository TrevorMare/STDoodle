namespace Doodle.Themes
{

    public class Bootstrap : Abstractions.Interfaces.ITheme
    {

        public string ToolbarWrapperClass { get; set; } = "";

        public string ToolbarHeaderClass { get; set; }  = "";

        public string ToolbarContentWrapperClass { get; set; }  = "";
        
        public string ToolbarContentClass { get; set; }  = "";

        public string ToolbarMenuButtonClass { get; set; } = "btn btn-default";
       
        public string ToolbarMenuButtonsContainerClass { get; set; } = "";

        public string ToolbarMenuButtonsClass { get; set; } = "";

        public string ToolbarContentPanelsClass { get; set; } = "";

        public string ToolbarContentPanelClass { get; set; } = "";

        public string ToolbarContentPanelHeaderClass { get; set; } = "";

        public string ToolbarContentPanelHeaderTextClass { get; set; } = "";

        public string ToolbarContentPanelContentClass { get; set; } = "";

        public string ToolbarContentPanelContentWrapperClass { get; set; } = "";

        public string ToolbarContentPanelContentCloseClass { get; set; } = "";
    }

}