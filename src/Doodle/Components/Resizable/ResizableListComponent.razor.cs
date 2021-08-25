using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Doodle.Components.Resizable
{

    public partial class ResizableListComponent : Shared.DoodleBaseComponent
    {

        #region Parameters
        
        [Parameter]
        public string ResizeContainerClass { get; set; }

        [Parameter]
        public IEnumerable<Abstractions.Interfaces.IResizableContent> Content { get; set; }

        [Parameter]
        public bool Active { get; set; } = false;
        #endregion

        #region Config Init
        protected override void OnInitialized()
        {
            this.DoodleDrawInteraction.OnClearDoodle += (s, clearHistory) => 
            {
                this.Content = new List<Abstractions.Interfaces.IResizableContent>();
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

        protected override void InitConfigSettings(Abstractions.Config.DoodleDrawConfig config)
        {
            this.Content = new List<Abstractions.Interfaces.IResizableContent>() 
            {
                new Abstractions.Models.ResizableText() { Height = 20, Left = 50, Top = 50, Text = "This is my Text 1", Width = 100 },
                new Abstractions.Models.ResizableText() { Height = 20, Left = 50, Top = 200, Text = "This is my Text 2", Width = 100 },
                new Abstractions.Models.ResizableImage() { Height = 100, Left = 200, Top = 200, ImageSource = "./_content/STDoodle/img/svg1.svg", Width = 100, MinHeight=50, MinWidth=50 }
            };

            if (config == null || config.ResizableContainerConfig == null) return;

            this.ResizeContainerClass = config.ResizableContainerConfig.ResizeContainerClass;
        }
        #endregion

    }
}