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

        string BackgroundPickerSourceImageClass { get; set; }

        string CanvasResizeContainerClass { get; set; }

        string CanvasDrawClass { get; set; }

        string CanvasPreviewDrawClass { get; set; }
        
        string ColorPickerWrapperClass { get; set; }
        
        string ColorPickerFavouritesWrapperClass { get; set; }

        string ColorPickerFavouriteOuterClass { get; set; }

        string ColorPickerFavouriteInnerClass { get; set; }

        string ColorPickerCustomColorWrapperClass { get; set; }

        string ColorPickerCustomColorInputClass { get; set; }

        string SizePickerWrapperClass { get; set; } 
        
        string SizePickerFavouritesWrapperClass { get; set; } 

        string SizePickerFavouriteOuterClass { get; set; } 

        string SizePickerFavouriteInnerClass { get; set; } 

        string SizePickerCustomDrawSizeWrapperClass { get; set; } 

        string SizePickerCustomEraserSizeWrapperClass { get; set; } 
        

    }

}