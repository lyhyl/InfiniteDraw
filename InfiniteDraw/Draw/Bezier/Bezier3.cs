using InfiniteDraw.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace InfiniteDraw.Draw.Bezier
{
    public partial class Bezier3 : Drawable
    {
        private partial class ControlPoint { }

        private const string defaultName = nameof(Bezier3);
        private const int defaultPrecision = 24;

        private List<Vector> controlPoints = new List<Vector>();

        public string Name { set; get; } = defaultName;
        public int Precision { set; get; } = defaultPrecision;

        public Bezier3()
        {
            controlPoints.Add(new Vector(-50, -50));
            controlPoints.Add(new Vector(0, -50));
            controlPoints.Add(new Vector(50, 0));
            controlPoints.Add(new Vector(50, 50));
        }

        public void Update(IEnumerable<Vector> vs)
        {
            controlPoints = new List<Vector>(vs);
        }

        public override void Draw(Graphics g, int depth, Matrix m, WorkMode editMode)
        {
            switch (editMode)
            {
                case WorkMode.Edit:
                    DrawBase(g, m);
                    DrawExtend(g, depth, m, editMode);
                    break;
                case WorkMode.Render:
                    DrawBase(g, m);
                    break;
                default:
                    break;
            }
        }

        private void DrawBase(Graphics g, Matrix m)
        {
            int DivSeg = GetPrecision();
            Pen pen = new Pen(Color.Black, 1);
            for (int i = 0; i + 3 < controlPoints.Count; i += 4)
            {
                PointF[] pts = BezierSolver.Divide(controlPoints, i, DivSeg);
                m.TransformPoints(pts);
                g.DrawLines(pen, pts);
            }
        }

        private void DrawExtend(Graphics g, int depth, Matrix m, WorkMode editMode)
        {
            Pen pen = new Pen(Color.Red, 0.25f);

            PointF[] cps = controlPoints.ConvertAll(p => p.ToPointF()).ToArray();
            m.TransformPoints(cps);

            foreach (var p in cps)
                g.DrawEllipse(pen, p.X - 1, p.Y - 1, 2, 2);

            RectangleF box = MeasureSize(depth, m, editMode);
            g.DrawRectangle(pen, box.Left, box.Top, box.Width, box.Height);
        }

        public override RectangleF MeasureSize(int depth, Matrix m, WorkMode editMode)
        {
            /// TODO : Dynamic Width
            const float MaxWidth = 1;

            if (controlPoints.Count == 0)
                return RectangleF.Empty;

            PointF[] cps = controlPoints.ConvertAll(p => p.ToPointF()).ToArray();
            m.TransformPoints(cps);
            float l = cps[0].X, r = cps[0].X, t = cps[0].Y, b = cps[0].Y;
            for (int i = 1; i < cps.Length; i++)
            {
                l = Math.Min(l, cps[i].X);
                r = Math.Max(r, cps[i].X);
                t = Math.Min(t, cps[i].Y);
                b = Math.Max(b, cps[i].Y);
            }
            RectangleF region = new RectangleF(l, t, r - l, b - t);
            //region.Inflate(MaxWidth, MaxWidth);
            return region;
        }

        private int GetPrecision()
        {
            return Precision;
        }

        public override string ToString() => Name + base.ToString();
    }
}
