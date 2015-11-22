using InfiniteDraw.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteDraw.Draw.Bezier
{
    public class Bezier3 : Drawable
    {
        private List<Vector> ctrlps = new List<Vector>();
        private Bitmap cacheImage = null;
        private RectangleF cacheSize = Rectangle.Empty;

        public Bezier3()
        {
            ctrlps.Add(new Vector(-50, -50));
            ctrlps.Add(new Vector(0, -50));
            ctrlps.Add(new Vector(50, 0));
            ctrlps.Add(new Vector(50, 50));
        }

        public void Update(IEnumerable<Vector> vs)
        {
            cacheImage = null;
            cacheSize = Rectangle.Empty;
            ctrlps = new List<Vector>(vs);
        }

        public override Bitmap Draw(int depth)
        {
            const int DivSeg = 16;

            if (cacheImage != null)
                return cacheImage;

            RectangleF size = MeasureSize(depth);
            if (size == RectangleF.Empty)
                return null;

            cacheImage = new Bitmap(
                (int)(Math.Ceiling(size.Right) - Math.Floor(size.Left)),
                (int)(Math.Ceiling(size.Bottom) - Math.Floor(size.Top)),
                PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(cacheImage);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.TranslateTransform(-size.Left, -size.Top);
            for (int i = 0; i + 3 < ctrlps.Count; i += 4)
            {
                var points = BezierSolver.Divide(ctrlps[i], ctrlps[i + 1], ctrlps[i + 2], ctrlps[i + 3], DivSeg);
                g.DrawLines(Pens.Black, points);
            }
            return cacheImage;
        }

        public override RectangleF MeasureSize(int depth)
        {
            const double w = 1;

            if (cacheSize != RectangleF.Empty)
                return cacheSize;

            if (ctrlps.Count == 0)
                return RectangleF.Empty;

            double l = ctrlps[0].X, r = ctrlps[0].X, t = ctrlps[0].Y, b = ctrlps[0].Y;
            for (int i = 1; i < ctrlps.Count; i++)
            {
                l = Math.Min(l, ctrlps[i].X);
                r = Math.Max(r, ctrlps[i].X);
                t = Math.Min(t, ctrlps[i].Y);
                b = Math.Max(b, ctrlps[i].Y);
            }
            l = Math.Floor(l - w);
            r = Math.Ceiling(r + w);
            t = Math.Floor(t - w);
            b = Math.Ceiling(b + w);

            cacheSize = new RectangleF((float)l, (float)t, (float)(r - l), (float)(b - t));
            return cacheSize;
        }

        public override string ToString()
        {
            return "Bezier" + base.ToString();
        }
    }
}
