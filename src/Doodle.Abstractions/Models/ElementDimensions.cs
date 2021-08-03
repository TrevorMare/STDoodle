namespace Doodle.Abstractions.Models
{

    public class ElementDimensions
    {

        #region Properties
        public double Height { get; set; }

        public double Width { get; set; }

        public double Top { get; set; }

        public double Left { get; set; }
        #endregion

        #region ctor
        public ElementDimensions(double top = 50, double left = 50, double width = 100, double height = 20)
        {
            this.Top = top;
            this.Left = left;
            this.Width = width;
            this.Height = height;
        }
        #endregion

    }


}