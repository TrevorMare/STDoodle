using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Doodle.Components.Shared
{

    public abstract class DoodleBaseComponent : ComponentBase
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
                    InitBaseConfigSettings(value);
                }
            } 
        }

        public Abstractions.Interfaces.ITheme Theme { get; private set; }
        #endregion

        #region Properties
        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> AdditionalAttributes { get; set; }

        [CascadingParameter(Name = "DoodleDrawInteraction")]
        public Abstractions.Interfaces.IDoodleDrawInteraction DoodleDrawInteraction { get; set; }

        [CascadingParameter(Name = "DataAttributeName")]
        public string DataAttributeName { get; set; }

        [CascadingParameter(Name = "Options")]
        public Abstractions.Config.DoodleDrawConfig Options 
        { 
            get => _options; 
            set 
            {
                if (_options != value)
                {
                    _options = value;
                    InitBaseConfigSettings(value);
                }
            } 
        }
        #endregion

        #region Methods
        private void InitBaseConfigSettings(Abstractions.Config.DoodleDrawConfig config)
        {
            if (config != null)
            {
                this.Theme = config.Theme;
            }
            InitConfigSettings(config);
        }

        protected virtual void InitConfigSettings(Abstractions.Config.DoodleDrawConfig config)
        {
        }
        #endregion

    }
}