using System;
using System.Threading.Tasks;
using Doodle.Abstractions.Models;
using Microsoft.AspNetCore.Components;

namespace Doodle.Components.Background
{

    public partial class BackgroundSourceComponent : Shared.DoodleBaseComponent
    {

        #region Properties
        [Parameter]
        public bool Visible { get; set; } = true;

        [Parameter]
        public BackgroundData BackgroundSource { get; set; }

        [Parameter]
        public bool IsPreview { get; set; } = false;

        [Parameter]
        public bool IsSelected { get; set; } = false;

        [Parameter]
        public EventCallback<BackgroundData> OnClick { get; set; }
        #endregion

        #region Methods
        private async Task OnBackgroundSourceClick(BackgroundData backgroundData)
        {
            await OnClick.InvokeAsync(backgroundData);
        }
        #endregion

    }


}