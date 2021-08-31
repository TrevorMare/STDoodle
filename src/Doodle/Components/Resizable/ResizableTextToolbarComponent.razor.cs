using System.Linq;
using System.Threading.Tasks;
using Doodle.State;
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
            var items = DoodleDrawInteraction.DoodleStateManager.ResizableContent.ToList();

            items.Add(new Abstractions.Models.ResizableText() 
            {
                Text = "My Text", Top = 100, Left = 100
            });
            await  DoodleDrawInteraction.DoodleStateManager.PushResziableState(new ResizableState(items));
        }
        #endregion

    }

}