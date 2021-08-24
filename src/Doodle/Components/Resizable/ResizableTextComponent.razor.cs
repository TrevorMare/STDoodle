using Microsoft.AspNetCore.Components;

namespace Doodle.Components.Resizable
{

    public partial class ResizableTextComponent : Shared.DoodleBaseComponent
    {
        
        #region Members
        private bool _elementActive = false;
        private Abstractions.Models.ResizableText Model => (Abstractions.Models.ResizableText)DataSource;
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
            this.DoodleDrawInteraction.OnDrawTypeChanged += (s, drawType) => {
                this.ElementActive = (drawType == Abstractions.Common.DrawType.ResizableText);
            };

            base.OnInitialized();
        }
        #endregion

    }

}