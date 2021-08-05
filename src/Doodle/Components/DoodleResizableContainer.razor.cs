using Microsoft.AspNetCore.Components;

namespace Doodle.Components
{
    public partial class DoodleResizableContainer : ComponentBase
    {

        #region Members
        private Abstractions.Config.DoodleDrawConfig _config;
        
        private Abstractions.Config.DoodleDrawConfig _options;

        [Inject]
        private Abstractions.Config.DoodleDrawConfig Config 
        { 
            get => _config; 
            set
            {
                if (_config != value)
                {
                    _config = value;
                    InitConfigSettings(value);
                }
            } 
        }
        #endregion

        #region Parameters
        
        [Parameter]
        public string ResizeContainerClass { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public Abstractions.Config.DoodleDrawConfig Options 
        { 
            get => _options; 
            set 
            {
                if (_options != value)
                {
                    _options = value;
                    InitConfigSettings(value);
                }
            } 
        }
        #endregion


        #region Config Init
        private void InitConfigSettings(Abstractions.Config.DoodleDrawConfig config)
        {
            if (config == null || config.ResizableContainerConfig == null) return;

            this.ResizeContainerClass = config.ResizableContainerConfig.ResizeContainerClass;
        }
        #endregion

    }
}