using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Doodle.Components.Resizable
{

    public partial class ResizableTextToolbarComponent : Shared.DoodleBaseComponent
    {

        #region Parameters
        [Parameter]
        public EventCallback OnCloseMenu { get; set; }
        #endregion

        #region Methods
        private async Task AddResizableText()
        {
            await DoodleDrawInteraction.AddResizableContent(new Abstractions.Models.ResizableText() 
            {
                Text = "My Text", Top = 100, Left = 100
            });
        }
        #endregion

    }

}