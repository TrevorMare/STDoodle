using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doodle.State;
using Microsoft.AspNetCore.Components;

namespace Doodle.Components.Resizable
{

    public partial class ResizableListComponent : Shared.DoodleBaseComponent
    {

        #region Parameters
        public IEnumerable<Abstractions.Interfaces.IResizableContent> Content => DoodleDrawInteraction.DoodleStateManager.ResizableContent;

        [Parameter]
        public bool Active { get; set; } = false;
        #endregion

        #region Config Init
        protected override void OnInitialized()
        {
            this.DoodleDrawInteraction.DoodleStateManager.OnRestoreState += (s, e) =>  {
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

        private async Task RemoveElement(Abstractions.Interfaces.IResizableContent item)
        {
            if (item != null)
            {
                var items = this.Content.ToList();
                items.Remove(item);
                
                await DoodleDrawInteraction.DoodleStateManager.PushResziableState(new State.ResizableState(items));
            }
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