using Microsoft.AspNetCore.Components;

namespace Doodle.Components
{

    public partial class DoodleResizable : ComponentBase
    {

        #region Properties
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool ResizeActive { get; set; } = false;

        [Parameter]
        public double Height { get; set; } = 50;

        [Parameter]
        public double Width { get; set; } = 100;
        #endregion


    }


}