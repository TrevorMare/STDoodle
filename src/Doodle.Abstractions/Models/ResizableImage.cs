using System;
using Doodle.Abstractions.Common;

namespace Doodle.Abstractions.Models
{

    public class ResizableImage : ElementDimensions, Interfaces.IResizableContent
    {

        #region Properties
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string ImageSource { get; set; }

        public string ImageClass { get; set; }

        public ResizableContentType ResizableContentType => ResizableContentType.Image;
        #endregion

    }


}