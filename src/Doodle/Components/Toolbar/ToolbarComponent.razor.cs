using Microsoft.AspNetCore.Components;

namespace Doodle.Components.Toolbar
{

    public partial class ToolbarComponent : ComponentBase
    {

        [Parameter]
        public bool IsMenuOpen { get; set; } = false;




    }
}