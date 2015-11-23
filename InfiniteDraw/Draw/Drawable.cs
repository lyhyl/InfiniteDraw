using System.Drawing;
using System.Drawing.Drawing2D;

namespace InfiniteDraw.Draw
{
    public abstract class Drawable
    {
        private static int AutoCount = 0;

        public int GID { get; } = AutoCount++;

        /// <summary>
        /// Get transformed size
        /// </summary>
        /// <param name="depth">Begining level</param>
        /// <param name="m">Transform matrix</param>
        /// <returns></returns>
        public abstract RectangleF MeasureSize(int depth, Matrix m);

        /// <summary>
        /// Draw on graphics
        /// </summary>
        /// <param name="depth">Begining level</param>
        /// <param name="g">Graphics to draw</param>
        public abstract void Draw(int depth, Graphics g);

        public override string ToString() => GID.ToString();
    }
}
