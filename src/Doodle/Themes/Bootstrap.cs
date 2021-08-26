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

    }

}