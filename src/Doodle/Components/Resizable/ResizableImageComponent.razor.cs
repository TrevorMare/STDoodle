using Microsoft.AspNetCore.Components;

namespace Doodle.Components.Resizable
{

    public partial class ResizableImageComponent
    {

        #region Members
        private Abstractions.Models.ResizableImage Model => (Abstractions.Models.ResizableImage)DataSource;
        #endregion
        
        #region Properties
        [Parameter]
        public Abstractions.Interfaces.IResizableContent DataSource { get; set; }

        [Parameter]
        public EventCallback<Abstractions.Interfaces.IResizableContent> DataSourceChanged { get; set; }

        #endregion

    }

}