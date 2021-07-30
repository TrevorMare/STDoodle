using System.Collections.Generic;

namespace Doodle.Abstractions.Config
{

    public class ColorPickerConfig
    {

        public IEnumerable<string> FavouriteColors { get; set; } = new List<string>() 
        {
            "#ff4000", "#ff8000", "#00ff00", "#00ffff", "#0000ff", "#bf00ff", "#ff0080"
        }; 

        public Common.Orientation Orientation { get; set; } = Common.Orientation.Horizontal;

        public string WrapperClass { get; set; }
        
        public string FavouriteWrapperClass { get; set; }

        public string FavouriteOuterClass { get; set; }

        public string FavouriteInnerClass { get; set; }

        public string CustomWrapperClass { get; set; }

        public bool Visible { get; set; } = true;

        public string ColorInputClass { get; set; }

        public bool ShowCustomColor { get; set; } = true;
    }

}