using InfiniteDraw.Utilities;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace InfiniteDraw.Draw.Base
{
    public partial class RefElement : Drawable
    {
        private ElementStorage elements;

        public int Reference { set; get; }
        public Vector Position { set; get; } = Vector.Zero;
        public Vector BaseTransform { set; get; } = Vector.XAxis;

        public RefElement(ElementStorage es, int r)
        {
            elements = es;
            Reference = r;
        }

        public override void Draw(Graphics g, int depth, Matrix m, WorkMode editMode)
        {
            Matrix mx = TransformMatrix;
            mx.Multiply(m);
            GetReference().Draw(g, depth, mx, editMode);

            if (editMode == WorkMode.Edit && depth <= 1)
                DrawArrow(g, mx);
        }

        public override RectangleF MeasureSize(int depth, Matrix m, WorkMode editMode)
        {
            Matrix mx = TransformMatrix;
            mx.Multiply(m);
            return GetReference().MeasureSize(depth, mx, editMode);
        }

        private static void DrawArrow(Graphics g, Matrix m)
        {
            PointF[] pts = new PointF[] { new Point(0, 0), new Point(0, 20), new Point(2, 15), new Point(-2, 15) };
            m.TransformPoints(pts);
            g.DrawLine(Pens.Green, pts[0], pts[1]);
            g.DrawLine(Pens.Green, pts[1], pts[2]);
            g.DrawLine(Pens.Green, pts[1], pts[3]);
        }

        private Drawable GetReference()
        {
            Drawable reference;
            try
            {
                reference = elements[Reference];
            }
            catch (Exception e)
            {
                throw new ArgumentException("Invalid GID", e);
            }
            return reference;
        }

        private Matrix TransformMatrix
        {
            get
            {
                Matrix m = new Matrix();
                float scale = (float)BaseTransform.Length;
                m.Translate((float)Position.X, (float)Position.Y);
                m.Rotate((float)(Math.Atan2(BaseTransform.Y, BaseTransform.X) * 180.0 / Math.PI));
                m.Scale(scale, scale);
                return m;
            }
        }
    }
}
