using System.Drawing;

namespace InfiniteDraw.Draw
{
    public interface IDrawable
    {
        Rectangle MeasureSize();
        Bitmap Draw(int depth);
        string Name { get; }
    }
}
