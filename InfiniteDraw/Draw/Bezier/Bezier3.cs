using InfiniteDraw.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfiniteDraw.Draw.Bezier
{
    public class Bezier3 : Drawable
    {
        private const string defaultName = nameof(Bezier3);
        private const int defaultPrecision = 16;

        private List<Vector> ctrlps = new List<Vector>();

        public string Name { set; get; } = defaultName;
        public int Precision { set; get; } = defaultPrecision;

        public Bezier3()
        {
            ctrlps.Add(new Vector(-50, -50));
            ctrlps.Add(new Vector(0, -50));
            ctrlps.Add(new Vector(50, 0));
            ctrlps.Add(new Vector(50, 50));
        }

        public void Update(IEnumerable<Vector> vs)
        {
            ctrlps = new List<Vector>(vs);
        }

        public override void Draw(int depth, Graphics g)
        {
            /// TODO : Auto Precision
            int DivSeg = Precision;
            for (int i = 0; i + 3 < ctrlps.Count; i += 4)
            {
                var points = BezierSolver.Divide(ctrlps[i], ctrlps[i + 1], ctrlps[i + 2], ctrlps[i + 3], DivSeg);
                Pen p = new Pen(Color.Black, 1);
                g.DrawLines(p, points);
            }
        }

        public override RectangleF MeasureSize(int depth, Matrix m)
        {
            /// TODO : Dynamic Width
            const float MaxWidth = 1;

            if (ctrlps.Count == 0)
                return RectangleF.Empty;

            PointF[] ctr = ctrlps.ConvertAll((v) => { return v.ToPointF(); }).ToArray();
            m.TransformPoints(ctr);
            float l = ctr[0].X, r = ctr[0].X, t = ctr[0].Y, b = ctr[0].Y;
            for (int i = 1; i < ctr.Length; i++)
            {
                l = Math.Min(l, ctr[i].X);
                r = Math.Max(r, ctr[i].X);
                t = Math.Min(t, ctr[i].Y);
                b = Math.Max(b, ctr[i].Y);
            }
            return new RectangleF(l - MaxWidth, t - MaxWidth, r - l + MaxWidth * 2, b - t + MaxWidth * 2);
        }

        public override string ToString() => Name + base.ToString();
    }
}
