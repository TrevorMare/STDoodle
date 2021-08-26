namespace Doodle.Abstractions.Interfaces
{

    public interface ITheme 
    {

        string ToolbarWrapperClass { get; set; }

        string ToolbarHeaderClass { get; set; }

        string ToolbarContentWrapperClass { get; set; } 

        string ToolbarContentClass { get; set; }

        string ToolbarContentPanelsClass { get; set; }

        string ToolbarContentPanelClass { get; set; }

        string ToolbarContentPanelHeaderClass { get; set; }

        string ToolbarContentPanelHeaderTextClass { get; set; }

        string ToolbarContentPanelContentClass { get; set; }

        string ToolbarContentPanelContentWrapperClass { get; set; }

        string ToolbarContentPanelContentCloseClass { get; set; }

        string ToolbarMenuButtonClass { get; set; }

        string ToolbarMenuButtonsContainerClass { get; set; }

        string ToolbarMenuButtonsClass { get; set; } 
        
    }

}