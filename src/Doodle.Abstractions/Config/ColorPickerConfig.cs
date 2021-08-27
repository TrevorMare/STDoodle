using System.Collections.Generic;

namespace Doodle.Abstractions.Config
{

    public class ColorPickerConfig
    {

        /// <summary>
        /// Gets or sets the favourite colors
        /// </summary>
        /// <typeparam name="string"></typeparam>
        /// <returns></returns>
        public IEnumerable<string> FavouriteColors { get; set; } = new List<string>() 
        {
            "#fff", "#c4c4c4", "#9c9c9c", "#5c5c5c", "#303030", "#000",
            "#ffffbe", "#ffff99", "#ffff4d", "#ffff00", "#b3b300", "#666600", 
            "#ffe7be", "#ffdb99", "#ffc14d", "#ffa500", "#b37400", "#664200", 
            "#f6dbc6", "#f3c6a5", "#ea9a62", "#e06f1f", "#9d4e15", "#70380f", 
            "#ffbebe", "#ff9999", "#ff4d4d", "#ff0000", "#b30000", "#660000", 
            "#ffb3be", "#ffb3bf", "#ff6680", "#ff1a40", "#cc0022", "#800015", 
            "#ffc2ff", "#ff99ff", "#ff4dff", "#ff00ff", "#b300b3", "#660066", 
            "#e1c9f8", "#cda5f3", "#a862ea", "#8a2be2", "#6918b4", "#410f70",
            "#c2c2ff", "#9999ff", "#4d4dff", "#0000ff", "#0000b3", "#000066",
            "#c2ffc2", "#99ff99", "#4dff4d", "#00ff00", "#00b300", "#006600"
        }; 


        /// <summary>
        /// Gets or sets a value indicating if the custom colors should be allowed
        /// </summary>
        /// <value></value>
        public bool ShowCustomColor { get; set; } = true;
    }

}