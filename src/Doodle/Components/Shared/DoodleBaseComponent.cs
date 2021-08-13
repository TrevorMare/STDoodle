using Microsoft.AspNetCore.Components;

namespace Doodle.Components.Shared
{

    public abstract class DoodleBaseComponent : ComponentBase
    {

        #region Members
        private Abstractions.Config.DoodleDrawConfig _config;

        private Abstractions.Config.DoodleDrawConfig _options;
        #endregion

        #region Properties
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
                    InitConfigSettings(value);
                }
            } 
        }

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

        #region Methods
        protected virtual void InitConfigSettings(Abstractions.Config.DoodleDrawConfig config)
        {

        }
        #endregion

    }
}