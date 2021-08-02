using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;

namespace Doodle.Components 
{

    public partial class DoodleBackground : ComponentBase
    {

        #region Properties
        [Parameter]
        public bool Visible { get; set; } = true;

        [Parameter]
        public string BackgroundSource { get; set; } = "./_content/STDoodle/img/mono-kpresenter-kpr.svg";

        [Parameter]
        public string BackgroundClass { get; set; }
        #endregion
        
    }
}