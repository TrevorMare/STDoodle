namespace Doodle.Abstractions.Interfaces
{

    public interface IResizableContent 
    {

        #region Properties
        Common.ResizableContentType ResizableContentType { get; }

        double Height { get; set; }

        double Width { get; set; }

        double Top { get; set; }

        double Left { get; set; }

        double? MinWidth { get; set; }

        double? MinHeight { get; set; }
        #endregion

    }


}