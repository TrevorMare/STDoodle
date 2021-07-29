using System.Collections.Generic;

namespace Doodle.Abstractions.Config
{

    public class SizePickerConfig
    {

        public IEnumerable<int> FavouriteSizes { get; set; } = new List<int>() 
        {
            1, 2, 4, 6
        }; 

        public Common.Orientation Orientation { get; set; } = Common.Orientation.Horizontal;

        public string WrapperClass { get; set; }
        
        public string FavouriteWrapperClass { get; set; }

        public string FavouriteOuterClass { get; set; }

        public string FavouriteInnerClass { get; set; }

        public string CustomWrapperClass { get; set; }

        public bool Visible { get; set; } = true;
    }

}