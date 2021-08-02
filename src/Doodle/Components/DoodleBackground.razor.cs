using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Doodle.Abstractions.Models;
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
        public BackgroundData BackgroundSource { get; set; }
        #endregion
        
    }
}