using System.Drawing;

namespace InfiniteDraw.Draw
{
    public abstract class Drawable
    {
        public abstract Rectangle MeasureSize();
        protected abstract Bitmap Draw(int depth);
    }
}
