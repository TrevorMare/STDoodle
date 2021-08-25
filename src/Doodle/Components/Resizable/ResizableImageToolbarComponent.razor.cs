using System.Collections.Generic;
using System.Threading.Tasks;
using Doodle.Abstractions.Config;
using Doodle.Abstractions.Models;
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
            await DoodleDrawInteraction.AddResizableContent(new Abstractions.Models.ResizableImage() 
            {
                Height = 100, Width = 100, Top = 100, Left = 100, ImageSource = resizableImageSource.DataSource 
            });
        }

        protected override void InitConfigSettings(DoodleDrawConfig config)
        {
            base.InitConfigSettings(config);

            this.ImageSources = new List<ResizableImageSource>()
            {
                new ResizableImageSource() { DataSource = "./_content/STDoodle/img/svg1.svg" },
                new ResizableImageSource() { DataSource = "./_content/STDoodle/img/svg2.svg" },
                new ResizableImageSource() { DataSource = "./_content/STDoodle/img/svg3.svg" },
                new ResizableImageSource() { DataSource = "./_content/STDoodle/img/svg4.svg" }
            };

        }
        #endregion

    }

}