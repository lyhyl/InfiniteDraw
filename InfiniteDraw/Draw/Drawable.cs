using System.Drawing;
using System.Drawing.Drawing2D;

namespace InfiniteDraw.Draw
{
    public abstract class Drawable
    {
        private static int AutoCount = 0;

        public int GID { get; } = AutoCount++;

        public abstract RectangleF MeasureSize(int depth, Matrix m);
        public abstract void Draw(int depth, Graphics g);
        public abstract RectangleF MeasureEditSize(int depth, Matrix m);
        public abstract void EditDraw(int depth, Graphics g);

        public RectangleF MeasureSize(Matrix m) => MeasureSize(0, m);
        public void Draw(Graphics g) => Draw(0, g);
        public RectangleF MeasureEditSize(Matrix m) => MeasureEditSize(0, m);
        public void EditDraw(Graphics g) => EditDraw(0, g);

        public override string ToString() => GID.ToString();
    }
}
