using InfiniteDraw.Edit.Draw;
using InfiniteDraw.Utilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        protected RefElement()
        {
        }

        public override void Draw(int depth, Graphics g)
        {
            Matrix memo = g.Transform;
            g.Transform = GetTransformMatrix(memo);
            GetReference().Draw(depth, g);
            g.Transform = memo;
        }

        public override void EditDraw(int depth, Graphics g)
        {
            Matrix memo = g.Transform;
            g.Transform = GetTransformMatrix(memo);
            GetReference().EditDraw(depth, g);
            if (depth <= 1)
                DrawArrow(g);
            g.Transform = memo;
        }

        private static void DrawArrow(Graphics g)
        {
            g.DrawLine(Pens.Green, new Point(0, 0), new Point(0, 20));
            g.DrawLine(Pens.Green, new Point(0, 20), new Point(2, 15));
            g.DrawLine(Pens.Green, new Point(0, 20), new Point(-2, 15));
        }

        public override RectangleF MeasureSize(int depth, Matrix m)
        {
            Region = GetReference().MeasureSize(depth, GetTransformMatrix(m));
            return Region;
        }

        public override RectangleF MeasureEditSize(int depth, Matrix m)
        {
            Region = GetReference().MeasureEditSize(depth, GetTransformMatrix(m));
            return Region;
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

        private Matrix GetTransformMatrix(Matrix b)
        {
            Matrix m = b.Clone();
            float scale = (float)BaseTransform.Length;
            m.Translate((float)Position.X, (float)Position.Y);
            m.Rotate((float)(Math.Atan2(BaseTransform.Y, BaseTransform.X) * 180.0 / Math.PI));
            m.Scale(scale, scale);
            return m;
        }
    }
}
