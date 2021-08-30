using System;
using Doodle.Abstractions.Common;

namespace Doodle.Abstractions.Models
{

    public class ResizableText : ElementDimensions, Interfaces.IResizableContent 
    {

        #region Properties
        public string Id { get; set; } = Guid.NewGuid().ToString();
        
        public string Text { get; set; }

        public ResizableContentType ResizableContentType => ResizableContentType.Text;
        #endregion

    }


}