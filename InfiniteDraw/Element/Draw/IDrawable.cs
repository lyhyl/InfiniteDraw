using System.Drawing;
using System.Drawing.Drawing2D;

namespace InfiniteDraw.Element.Draw
{
    public enum WorkMode { Edit, Render }

    public interface IDrawable : IElement
    {
        int GID { set; get; }

        RectangleF MeasureSize(int depth, Matrix m, WorkMode workMode);
        void Draw(Graphics graphics, int depth, Matrix m, WorkMode workMode);
    }
}
