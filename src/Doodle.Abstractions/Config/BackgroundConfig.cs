using System.Collections.Generic;
using Doodle.Abstractions.Models;

namespace Doodle.Abstractions.Config
{

    public class BackgroundConfig
    {

        /// <summary>
        /// Gets or sets the sources available for the user to use as canvas backgrounds
        /// </summary>
        /// <value></value>
        public IEnumerable<Models.BackgroundData> BackgroundSources { get; set; }

        public BackgroundConfig()
        {
            this.BackgroundSources = new List<Abstractions.Models.BackgroundData>();
        }
        
    }

}