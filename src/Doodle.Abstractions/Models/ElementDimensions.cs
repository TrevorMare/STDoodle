namespace Doodle.Abstractions.Models
{

    public class ElementDimensions
    {

        #region Properties
        public double Height { get; set; } = 20;

        public double Width { get; set; } = 100;

        public double Top { get; set; } = 0;

        public double Left { get; set; } = 0;

        public double? MinWidth { get; set; }

        public double? MinHeight { get; set; }
        #endregion

        #region ctor
        public ElementDimensions()
        {
        }
        
        public ElementDimensions(double top = 0, double left = 0, double width = 100, double height = 20, double? minWidth = null, double? minHeight = null)
        {
            this.Top = top;
            this.Left = left;
            this.Width = width;
            this.Height = height;
            this.MinHeight = minHeight;
            this.MinWidth = minWidth;
        }
        #endregion

    }


}