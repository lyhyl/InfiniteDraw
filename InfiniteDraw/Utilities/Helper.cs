using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteDraw.Utilities
{
    public static class Helper
    {
        public static Matrix ImageToDrawing(Vector offset, Size clientSize)
        {
            Matrix m = new Matrix();
            m.Translate(clientSize.Width / 2.0f, clientSize.Height / 2.0f);
            m.Scale(1, -1);
            m.Translate((float)offset.X, (float)offset.Y);
            return m;
        }

        public static Matrix DrawingToImage(Vector offset, Size clientSize)
        {
            Matrix m = new Matrix();
            m.Translate((float)-offset.X, (float)-offset.Y);
            m.Scale(1, -1);
            m.Translate(-clientSize.Width / 2.0f, -clientSize.Height / 2.0f);
            return m;
        }
    }
}
