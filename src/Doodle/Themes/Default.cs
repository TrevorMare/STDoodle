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

    }

}