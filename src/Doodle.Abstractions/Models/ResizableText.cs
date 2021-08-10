using Doodle.Abstractions.Common;

namespace Doodle.Abstractions.Models
{

    public class ResizableText : ElementDimensions, Interfaces.IResizableContent 
    {

        #region Properties
        public string Text { get; set; }

        public ResizableContentType ResizableContentType => ResizableContentType.Text;
        #endregion

    }


}