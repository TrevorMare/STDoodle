using System.Collections.Generic;
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
        public IEnumerable<Abstractions.Interfaces.IResizableContent> Content { get; set; }

        [Parameter]
        public EventCallback<IEnumerable<Abstractions.Interfaces.IResizableContent>> ContentChanged { get; set; }

        [Parameter]
        public bool Active { get; set; } = false;

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