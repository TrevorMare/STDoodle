namespace Doodle.Abstractions.Models
{

    public class ElementDimensions
    {

        #region Properties
        public double Height { get; set; }

        public double Width { get; set; }

        public double Top { get; set; }

        public double Left { get; set; }

        public double? MinWidth { get; set; }

        public double? MinHeight { get; set; }
        #endregion

        #region ctor
        public ElementDimensions()
        {
        }
        
        public ElementDimensions(double top = 50, double left = 50, double width = 100, double height = 20, double? minWidth = null, double? minHeight = null)
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