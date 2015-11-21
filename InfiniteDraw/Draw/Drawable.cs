using System.Drawing;

namespace InfiniteDraw.Draw
{
    public abstract class Drawable
    {
        public int GID { private set; get; }
        private static int AutoCount = 0;

        public Drawable()
        {
            GID = AutoCount++;
        }

        /// <summary>
        /// Get approximately size of itself
        /// </summary>
        /// <param name="depth">Begining level</param>
        /// <returns></returns>
        public abstract Rectangle MeasureSize(int depth);

        /// <summary>
        /// Draw on Bitmap
        /// </summary>
        /// <param name="depth">Begining level</param>
        /// <returns>null for empty</returns>
        public abstract Bitmap Draw(int depth);

        public override string ToString()
        {
            return GID.ToString();
        }
    }
}
