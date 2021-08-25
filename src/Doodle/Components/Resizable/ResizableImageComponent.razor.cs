using System.Collections.Generic;
using Doodle.Abstractions.Models;
using Microsoft.AspNetCore.Components;

namespace Doodle.Components.Resizable
{

    public partial class ResizableImageComponent : Shared.DoodleBaseComponent
    {

        #region Members
        private bool _elementActive = false;
        private Abstractions.Models.ResizableImage Model => (Abstractions.Models.ResizableImage)DataSource;
        #endregion
        
        #region Properties
  
        [Parameter]
        public Abstractions.Interfaces.IResizableContent DataSource { get; set; }

        [Parameter]
        public EventCallback<Abstractions.Interfaces.IResizableContent> DataSourceChanged { get; set; }

        public bool ElementActive 
        { 
            get => _elementActive; 
            set
            {
                if (_elementActive != value)
                {
                    _elementActive = value;
                    ElementActiveChanged.InvokeAsync(value).ConfigureAwait(false);
                }
            } 
        }

        public EventCallback<bool> ElementActiveChanged { get; set; }
        #endregion

        #region Methods
        protected override void OnInitialized()
        {
            this.ElementActive = (DoodleDrawInteraction.DrawType == Abstractions.Common.DrawType.ResizableImage);
            this.DoodleDrawInteraction.OnDrawTypeChanged += (s, drawType) => {
                this.ElementActive = (drawType == Abstractions.Common.DrawType.ResizableImage);
            };
            base.OnInitialized();
        }
        #endregion

    }

}