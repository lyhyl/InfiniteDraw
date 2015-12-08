using System.Drawing;
using System.Drawing.Drawing2D;

namespace InfiniteDraw.Draw
{
    public enum WorkMode { Edit, Render }
    public abstract class Drawable
    {
        private static int AutoCount = 0;

        public int GID { get; } = AutoCount++;

        public abstract RectangleF MeasureSize(int depth, Matrix m, WorkMode editMode);
        public abstract void Draw(Graphics graphics, int depth, Matrix m, WorkMode editMode);

        public RectangleF MeasureSize(WorkMode editMode) => MeasureSize(0, new Matrix(), editMode);
        public void Draw(Graphics graphics, WorkMode editMode) => Draw(graphics, 0, new Matrix(), editMode);

        public override string ToString() => GID.ToString();
    }
}
