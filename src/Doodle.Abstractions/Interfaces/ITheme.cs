namespace Doodle.Abstractions.Interfaces
{

    public interface ITheme
    {

        string ToolbarWrapperClass { get; set; }

        string ToolbarHeaderClass { get; set; }

        string ToolbarContentWrapperClass { get; set; } 

        string ToolbarContentClass { get; set; }

        string ToolbarMenuButtonClass { get; set; }

        string ToolbarMenuButtonsContainerClass { get; set; }

        string ToolbarMenuButtonsClass { get; set; }
        
    }

}