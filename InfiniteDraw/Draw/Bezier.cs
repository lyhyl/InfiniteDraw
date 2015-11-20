using InfiniteDraw.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteDraw.Draw
{
    public class Bezier : IDrawable
    {
        private List<Vector> ctrlps = new List<Vector>();

        public string Name { set; get; }

        public Bezier()
        {
            ctrlps.Add(new Vector(0, 0));
            ctrlps.Add(new Vector(50, 0));
            ctrlps.Add(new Vector(100, 50));
            ctrlps.Add(new Vector(100, 100));
        }

        public Bitmap Draw(int depth)
        {
            Rectangle size = MeasureSize();
            Bitmap bmp = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            for (int i = 0; i + 3 < ctrlps.Count; i += 4)
                g.DrawLines(Pens.Black, BezierSolver.Div(ctrlps[i], ctrlps[i + 1], ctrlps[i + 2], ctrlps[i + 3], 10));
            return bmp;
        }

        public Rectangle MeasureSize()
        {
            double l = 0, r = 0, t = 0, b = 0;
            foreach (var v in ctrlps)
            {
                l = Math.Min(l, v.X);
                r = Math.Max(r, v.X);
                t = Math.Min(t, v.Y);
                b = Math.Max(b, v.Y);
            }
            int il = (int)Math.Floor(l), it = (int)Math.Floor(t);
            int w = (int)Math.Ceiling(r) - il, h = (int)Math.Ceiling(b) - it;
            return new Rectangle(il, it, w, h);
        }
    }
}
