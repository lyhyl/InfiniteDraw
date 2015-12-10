using System.Drawing;
using System.Drawing.Drawing2D;

namespace InfiniteDraw.Draw
{
    public enum WorkMode { Edit, Render }
    public abstract class Drawable
    {
        private static int AutoCount = 0;

        public int GID { get; } = AutoCount++;

        public abstract RectangleF MeasureSize(int depth, Matrix m, WorkMode workMode);
        public abstract void Draw(Graphics graphics, int depth, Matrix m, WorkMode workMode);

        public RectangleF MeasureSize(WorkMode workMode) => MeasureSize(0, new Matrix(), workMode);
        public void Draw(Graphics graphics, WorkMode workMode) => Draw(graphics, 0, new Matrix(), workMode);

        public override string ToString() => GID.ToString();
    }
}
