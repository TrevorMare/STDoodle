using Doodle.Abstractions.Common;

namespace Doodle.Abstractions.Models
{

    public class BackgroundData
    {

        public BackgroundSourceType BackgroundSourceType { get; set; } = BackgroundSourceType.Url;
        
        public string DataSource { get; set; }

        public string BackgroundClass { get; set; }

    }

}