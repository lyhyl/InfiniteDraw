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
    public partial class Bezier3 : Drawable
    {
        private partial class ControlPoint { }

        private const string defaultName = nameof(Bezier3);
        private const int defaultPrecision = 24;

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
            Pen pen = new Pen(Color.Black, 1);
            for (int i = 0; i + 3 < ctrlps.Count; i += 4)
            {
                var points = BezierSolver.Divide(ctrlps[i], ctrlps[i + 1], ctrlps[i + 2], ctrlps[i + 3], DivSeg);
                g.DrawLines(pen, points);
            }
        }

        public override void EditDraw(int depth, Graphics g)
        {
            Draw(depth, g);
            Pen pen = new Pen(Color.Red, 0.5f);
            foreach (var p in ctrlps)
                g.DrawEllipse(pen, (float)p.X - 1, (float)p.Y - 1, 2, 2);
            RectangleF box = MeasureEditSize(depth, g.Transform);
            Matrix memo = g.Transform;
            g.Transform = new Matrix();
            g.DrawRectangle(pen, box.Left, box.Top, box.Width, box.Height);
            g.Transform = memo;
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
            RectangleF region = new RectangleF(l, t, r - l, b - t);
            //region.Inflate(MaxWidth, MaxWidth);
            return region;
        }

        public override RectangleF MeasureEditSize(int depth, Matrix m)
        {
            RectangleF region = MeasureSize(depth, m);
            //region.Inflate(10, 10);
            return region;
        }

        public override string ToString() => Name + base.ToString();
    }
}
