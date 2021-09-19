using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Doodle.Abstractions.Config;
using Doodle.Abstractions.Models;
using Doodle.State;
using Microsoft.AspNetCore.Components;

namespace Doodle.Components.Resizable
{

    public partial class ResizableImageToolbarComponent : Shared.DoodleBaseComponent
    {

        #region Properties
        [Parameter]
        public EventCallback OnCloseMenu { get; set; }

        [Parameter]
        public IEnumerable<ResizableImageSource> ImageSources { get; set; } = new List<Abstractions.Models.ResizableImageSource>();
        #endregion

        #region Methods 
        private async Task AddResizableContentImage(Abstractions.Models.ResizableImageSource resizableImageSource)
        {

            var items = DoodleDrawInteraction.DoodleStateManager.ResizableContent.ToList();

            items.Add(new Abstractions.Models.ResizableImage() 
            {
                Height = 100, Width = 100, Top = 100, Left = 100, ImageSource = resizableImageSource.DataSource 
            });
            await  DoodleDrawInteraction.DoodleStateManager.PushResziableState(new ResizableState(items));
        }

        protected override void InitConfigSettings(DoodleDrawConfig config)
        {
            base.InitConfigSettings(config);

            if (config?.BackgroundConfig == null ) return;

            this.ImageSources = config.CanvasConfig.ResizableImages ?? new List<Abstractions.Models.ResizableImageSource>();
        }
        #endregion

    }

}