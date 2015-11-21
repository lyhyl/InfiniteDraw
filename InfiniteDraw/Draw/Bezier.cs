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
    public class Bezier : Drawable
    {
        private List<Vector> ctrlps = new List<Vector>();
        private Bitmap cache = null;

        public Bezier()
        {
            ctrlps.Add(new Vector(-50, -50));
            ctrlps.Add(new Vector(0, -50));
            ctrlps.Add(new Vector(50, 0));
            ctrlps.Add(new Vector(50, 50));
        }

        public void Update(IEnumerable<Vector> vs)
        {
            cache = null;
            ctrlps = new List<Vector>(vs);
        }

        public override Bitmap Draw(int depth)
        {
            if (cache != null)
                return cache;
            Rectangle size = MeasureSize(depth);
            Size offset = new Size(size.Width / 2, size.Height / 2);
            cache = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(cache);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            const int DivSeg = 16;
            for (int i = 0; i + 3 < ctrlps.Count; i += 4)
            {
                var points = BezierSolver.Divide(ctrlps[i], ctrlps[i + 1], ctrlps[i + 2], ctrlps[i + 3], DivSeg);
                for (int j = 0; j < points.Length; j++)
                    points[j] += offset;
                g.DrawLines(Pens.Black, points);
            }
            return cache;
        }

        public override Rectangle MeasureSize(int depth)
        {
            double l = 0, r = 0, t = 0, b = 0;
            foreach (var v in ctrlps)
            {
                l = Math.Min(l, v.X);
                r = Math.Max(r, v.X);
                t = Math.Min(t, v.Y);
                b = Math.Max(b, v.Y);
            }
            int w = (int)Math.Ceiling(Math.Max(Math.Abs(l), Math.Abs(r))) + 1;
            int h = (int)Math.Ceiling(Math.Max(Math.Abs(t), Math.Abs(b))) + 1;
            return new Rectangle(-w, -h, 2 * w, 2 * h);
        }

        public override string ToString()
        {
            return "Bezier" + base.ToString();
        }
    }
}
