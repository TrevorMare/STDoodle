using System.Collections.Generic;

namespace Doodle.Abstractions.Config
{

    public class SizePickerConfig
    {

        public IEnumerable<int> FavouriteSizes { get; set; } = new List<int>() 
        {
            1, 2, 3, 
            4, 5, 6,
            7, 8, 9
        }; 

        public string WrapperClass { get; set; }
        
        public string FavouriteWrapperClass { get; set; }

        public string FavouriteOuterClass { get; set; }

        public string FavouriteInnerClass { get; set; }

        public string CustomWrapperClass { get; set; }

        public bool Visible { get; set; } = true;

        public string NumberInputClass { get; set; }

        public bool ShowCustomNumber { get; set; } = true;

    }

}