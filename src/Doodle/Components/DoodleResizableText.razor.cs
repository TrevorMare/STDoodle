using Microsoft.AspNetCore.Components;

namespace Doodle.Components
{

    public partial class DoodleResizableText : ComponentBase
    {
        #region Members
        private Abstractions.Models.ResizableText Model => (Abstractions.Models.ResizableText)DataSource;
        #endregion
        
        #region Properties
        [Parameter]
        public Abstractions.Interfaces.IResizableContent DataSource { get; set; }

        [Parameter]
        public EventCallback<Abstractions.Interfaces.IResizableContent> DataSourceChanged { get; set; }
        #endregion
        
    }

}