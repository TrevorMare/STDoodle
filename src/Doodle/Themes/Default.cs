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

        public string ToolbarContentPanelContentCloseClass { get; set; } = "doodle-toolbar-close";

        public string BackgroundPickerSourceImageClass { get; set; } = "doodle-background-source-wrapper";

        public string CanvasResizeContainerClass { get; set; } = "";

        public string CanvasDrawClass { get; set; } = "doodle-canvas";

        public string CanvasPreviewDrawClass { get; set; } = "";

        public string ColorPickerWrapperClass { get; set; } = "";
        
        public string ColorPickerFavouritesWrapperClass { get; set; } = "";

        public string ColorPickerFavouriteOuterClass { get; set; } = "doodle-button-custom-outer";

        public string ColorPickerFavouriteInnerClass { get; set; } = "doodle-button-custom-inner";

        public string ColorPickerCustomColorWrapperClass { get; set; } = "";

        public string ColorPickerCustomColorInputClass { get; set; } = "";

        public string SizePickerWrapperClass { get; set; } = "";
        
        public string SizePickerFavouritesWrapperClass { get; set; } = "";

        public string SizePickerFavouriteOuterClass { get; set; } = "doodle-button-custom-outer"; 

        public string SizePickerFavouriteInnerClass { get; set; } = "doodle-button-custom-inner";

        public string SizePickerCustomDrawSizeWrapperClass { get; set; } = "";

        public string SizePickerCustomEraserSizeWrapperClass { get; set; } = "";

        

    }

}