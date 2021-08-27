using System.Collections.Generic;

namespace Doodle.Abstractions.Config
{

    public class SizePickerConfig
    {

        /// <summary>
        /// Gets or sets the favourite sizes
        /// </summary>
        /// <typeparam name="int"></typeparam>
        /// <returns></returns>
        public IEnumerable<int> FavouriteSizes { get; set; } = new List<int>() 
        {
            1, 2, 3, 
            4, 5, 6,
            8, 10, 12
        }; 

        /// <summary>
        /// Gets or sets a value indicating if the custom selection should be available
        /// </summary>
        /// <value></value>
        public bool ShowCustomNumber { get; set; } = true;

    }

}