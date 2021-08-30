using System.Collections.Generic;
using System.Threading.Tasks;
using Doodle.State;
using Microsoft.AspNetCore.Components;

namespace Doodle.Components.Resizable
{

    public partial class ResizableListComponent : Shared.DoodleBaseComponent
    {

        #region Parameters
        public IEnumerable<Abstractions.Interfaces.IResizableContent> Content => DoodleDrawInteraction.ResizableContents;

        [Parameter]
        public bool Active { get; set; } = false;
        #endregion

        #region Config Init
        protected override void OnInitialized()
        {
            this.DoodleDrawInteraction.OnClearDoodle += (s, clearHistory) => 
            {
                StateHasChanged();
            };

            this.DoodleDrawInteraction.OnDrawTypeChanged += (s, drawType) => {
                switch (drawType)
                {
                    case Abstractions.Common.DrawType.ResizableText:
                    case Abstractions.Common.DrawType.ResizableImage:
                    {
                        this.SetDrawTypeResizable();
                        break;
                    }
                    default:
                    {
                        this.SetDrawTypeNonResizable();
                        break;
                    }
                }

            };
            base.OnInitialized();
        }

        private async Task ContentUpdated()
        {
            await DoodleDrawInteraction.DoodleStateManager.PushResziableState(new ResizableState(this.Content));
        }

        private void SetDrawTypeResizable()
        {
            this.Active = true;
            StateHasChanged();
        }

        private void SetDrawTypeNonResizable()
        {
            this.Active = false;
            StateHasChanged();
        }
        #endregion

    }
}