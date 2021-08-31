using System;
using Doodle.Abstractions.Common;

namespace Doodle.Abstractions.Models
{

    public class BackgroundData
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public BackgroundSourceType BackgroundSourceType { get; set; } = BackgroundSourceType.Url;
        
        public string DataSource { get; set; }

        public string BackgroundClass { get; set; }

        public string Name { get; set; }
        

    }

}